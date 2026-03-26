/// Hack : ============================================================================
/// ⚠️ HACK NOTICE - NeraXTools TaskManager
/// ----------------------------------------------------------------------------
/// Active Tasks/Threads in C# cannot be forcibly cancelled!
/// - Do NOT try to stop running threads from C#.
/// - Replace forced cancel logic with **C++ native thread handling**. {add in another verssions}
///
/// Forceful termination must go through:
/// - C++ bridge / native code                      --> {add in another verssions}
/// - File or pipe interface for signaling
///
/// ============================================================================

using NeraXTools.LogManager;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Threading.Channels;

namespace NeraXTools.TaskManager
{
#pragma warning disable CS1591

    public static partial class TaskSchedulerEngine_Core
#pragma warning restore CS1591
    {
        private static int FirstRun = 1 /*true*/;

        private static Channel<IJobRequest> _FrameWorkPriority = Channel.CreateUnbounded<IJobRequest>();
        private static Channel<IJobRequest> _inFirstPriority = Channel.CreateUnbounded<IJobRequest>();
        private static Channel<IJobRequest> _inEndPriority = Channel.CreateUnbounded<IJobRequest>();
        private static Channel<IJobRequest> _highPriority = Channel.CreateUnbounded<IJobRequest>();
        private static Channel<IJobRequest> _midPriority = Channel.CreateUnbounded<IJobRequest>();
        private static Channel<IJobRequest> _LowPriority = Channel.CreateUnbounded<IJobRequest>();
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(Math.Max(1, Environment.ProcessorCount / 2));// Defualt Use half System Thereds

        private static readonly CancellationTokenSource _cts = new();

        //private static CancellationToken _Token => _cts.Token;
        //private static readonly Lazy<Task> _workerInitializer = new Lazy<Task>(() =>
        //(WorkerLoop));

        private static bool _workerStarted = false;
        private static int _activeJobs = 0;
        public static int MaxIdleDelayMs { get; set; } = 200;
        private static int BaseActiveDelayMs { get; set; } = 10;
        private static int BaseIdleDelayMs { get; set; } = 40;

        private static readonly Dictionary<int, int> _upgradeSkipMap = new()
                                                                     {
                                                                         { 5, 1 },//5
                                                                         { 9, 1 },//4
                                                                         { 12, 1 },//3
                                                                         { 14, 1 }//2
                                                                     };

        private static Process userAppPID = Process.GetProcessById(Environment.ProcessId);

        // -----------------------------
        // Configuration Methods
        // -----------------------------
        internal static void SetThreadsCount_ByPercent_Core(eThreadUsagePercent percent)
        {
            int processorCount = Environment.ProcessorCount;

            int threads =
                Math.Max(1, (int)Math.Ceiling(processorCount * ((double)percent / 100.0)));

            ResetSemaphore(threads);
        }

        internal static void SetThreadsCount_ByCore_Core(int coreCount)
        {
            int processorCount = Environment.ProcessorCount;

            int threads = coreCount * 2;

            if (threads > processorCount)
                threads = processorCount;

            if (threads < 1)
                threads = 1;

            ResetSemaphore(threads);
        }

        internal static void SetThreadsCount_ByThreads_Core(int threadCount)
        {
            int processorCount = Environment.ProcessorCount;

            if (threadCount > processorCount)
                threadCount = processorCount;

            if (threadCount < 1)
                threadCount = 1;

            ResetSemaphore(threadCount);
        }

        private static void ResetSemaphore(int count)
        {
            _semaphore?.Dispose();
            _semaphore = new SemaphoreSlim(count);
        }

        // -----------------------------
        // Job Request Interface
        // -----------------------------
        private interface IJobRequest
        {
            Task ExecuteAsync(CancellationToken token);
        }

        private class JobRequest : IJobRequest
        {
            private readonly Func<CancellationToken, Task> _job;
            private readonly TaskCompletionSource<object?> _tcs;

            public JobRequest(Func<CancellationToken, Task> job)
            {
                _job = job;
                _tcs = new TaskCompletionSource<object?>();
            }

            public Task jobTask => _tcs.Task;

            public async Task ExecuteAsync(CancellationToken token)
            {
                try
                {
                    Task jobTask = _job(token);

                    while (!jobTask.IsCompleted)
                    {
                        await Task.WhenAny(jobTask, Task.Delay(500, token));
                        token.ThrowIfCancellationRequested();
                    }

                    _tcs.TrySetResult(null);
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested)
                {
                    Logger.logForThisTool("Job Canceled", eLogType.Info, eLogRecordMode.UI);
                    _tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool("Job Error", eLogType.Exception, eLogRecordMode.UI);
                    _tcs.TrySetException(ex);
                }
            }
        }

        private class JobRequest<T> : IJobRequest
        {
            private readonly Func<CancellationToken, Task<T>> _job;

            private readonly TaskCompletionSource<T> _tcs;
            public Task<T> jobTask => _tcs.Task;

            public JobRequest(Func<CancellationToken, Task<T>> job)
            {
                _job = job;
                _tcs = new TaskCompletionSource<T>();
            }

            public async Task ExecuteAsync(CancellationToken token)
            {
                try
                {
                    Task<T> jobTask = _job(token);
                    while (!jobTask.IsCompleted)
                    {
                        await Task.WhenAny(jobTask, Task.Delay(500, token));
                        token.ThrowIfCancellationRequested();
                    }

                    _tcs.TrySetResult(await jobTask);
                }
                catch (OperationCanceledException)
                {
                    Logger.logForThisTool("Job Cancaled", eLogType.Info, eLogRecordMode.UI);
                    _tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool("Job Error", eLogType.Exception, eLogRecordMode.UI);
                    _tcs.TrySetException(ex);
                }
            }
        }

        // -----------------------------
        // Worker System
        // -----------------------------
        private static async Task StartWorker()
        {
            if (Interlocked.CompareExchange(ref _workerStarted, true, false) == true)
                return;
            //await _workerInitializer.Value;
            //_ = Task.Run(async () =>
            //{
            try
            {
                await WorkerLoop();
            }
            catch (Exception) { }
        }

        private static async Task WorkerLoop()
        {
            var queues = new[]
            {
                _FrameWorkPriority,
                _inFirstPriority,
                _highPriority,
                _midPriority,
                _LowPriority,
                _inEndPriority
            };

            int currentMaxLevel = 0;
            int emptyPassCount = 0;
            int maxPassBeforeEscalate = 2; // بخاطری دو که بار اول رد میشه و بار دوم چک میشه که یعنی خالی فعلا برو بعدی
            int upgrageLev = 0;
            while (!_cts.Token.IsCancellationRequested && !_cts.IsCancellationRequested)
            {
                bool executedSomething = false;

                for (int level = 0; level <= currentMaxLevel; level++)
                {
                    if (_upgradeSkipMap.TryGetValue(upgrageLev, out int skip))
                        if (skip > level)
                        {
                            level += skip;
                            if (level > currentMaxLevel)
                                level -= skip;
                        }
                        else if (upgrageLev > _upgradeSkipMap.Keys.Max())
                            upgrageLev = 0;
                    upgrageLev++;

                    while (queues[level].Reader.TryRead(out var job) /*&& maxCheackInOneTime <= 100*/)
                    {
                        executedSomething = true;
                        currentMaxLevel = 0;
                        emptyPassCount = 0;
                        //maxCheackInOneTime++;

                        await _semaphore.WaitAsync(_cts.Token);
                        Interlocked.Increment(ref _activeJobs);

                        var t = Task.Run(async () =>
                        {
                            try
                            {
                                await job.ExecuteAsync(_cts.Token);
                            }
                            //catch (OperationCanceledException)
                            //{
                            //    Logger.logForThisTool("Job Cancaled", LogTypeEunm.Info, LogRecordModeEnum.UI);
                            //}
                            catch (Exception ex)
                            {
                                Logger.logForThisTool("Job Error", eLogType.Exception, eLogRecordMode.UI);
                            }
                            finally
                            {
                                _semaphore.Release();
                                Interlocked.Decrement(ref _activeJobs);
                            }
                        }, _cts.Token);
                    }
                }

                int baseDelay = executedSomething ? BaseActiveDelayMs : BaseIdleDelayMs;
                if (!executedSomething)
                {
                    emptyPassCount++;

                    if (emptyPassCount >= maxPassBeforeEscalate &&
                        currentMaxLevel < queues.Length - 1)
                    {
                        currentMaxLevel++;
                        emptyPassCount = 0;
                    }
                }
                if (emptyPassCount > queues.Length)
                {
                    int delay = Math.Min(MaxIdleDelayMs, baseDelay + emptyPassCount * 5);
                    await Task.Delay(delay, _cts.Token);
                    if (delay >= MaxIdleDelayMs)
                    {
                        if (!ProgramOps.isExitedByPID(userAppPID.Id) && !ProgramOps.isExitedByName(userAppPID.ProcessName))
                        {
                            _cts.Cancel();
                        }
                    }
                }
                else
                    await Task.Delay(baseDelay, _cts.Token);
            }
        }

        internal static async Task<T> RunAsync_Core<T>(Func<CancellationToken, Task<T>> taskFunc, CancellationToken? cancellationToken_Local = default)
        {
            RunTaskSchedulerEngine();
            CancellationTokenSource? linked = null;
            try
            {
                await StartWorker(); // ONLY RUN IN FIRST TIME

                var request = new JobRequest<T>(taskFunc);
                // ترکیب token داخلی با token محلی
                CancellationToken combinedToken;

                if (cancellationToken_Local.HasValue)
                {
                    linked = CancellationTokenSource.CreateLinkedTokenSource(
                        _cts.Token,
                        cancellationToken_Local.Value);

                    combinedToken = linked.Token;
                }
                else
                {
                    combinedToken = _cts.Token;
                }
                await _midPriority.Writer.WriteAsync(request, combinedToken);

                return await request.jobTask;
            }
            catch (Exception ex)
            {
                Logger.logForThisTool($"Exception in RunAsync_Core<T>: {ex}");
                return default!;
            }
            finally
            {
                linked?.Dispose();
            }
        }

        internal static async Task RunAsync_Core(Func<CancellationToken, Task> taskFunc, eFrameworkPriorityLevel level = eFrameworkPriorityLevel.MidLevel, CancellationToken? cancellationToken_Local = default)
        {
            RunTaskSchedulerEngine();
            CancellationTokenSource? linked = null;
            try
            {
                await StartWorker();

                var request = new JobRequest(taskFunc);
                // ترکیب token داخلی با token محلی
                CancellationToken combinedToken;

                if (cancellationToken_Local.HasValue)
                {
                    linked = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, cancellationToken_Local.Value);
                    combinedToken = linked.Token;
                }
                else
                {
                    combinedToken = _cts.Token;
                }
                switch (level)
                {
                    case eFrameworkPriorityLevel.FrameworkLevel:
                        await _FrameWorkPriority.Writer.WriteAsync(request, combinedToken);
                        break;

                    case eFrameworkPriorityLevel.HighLevel:
                        await _highPriority.Writer.WriteAsync(request, combinedToken);
                        break;

                    default:
                    case eFrameworkPriorityLevel.MidLevel:
                        await _midPriority.Writer.WriteAsync(request, combinedToken);
                        break;

                    case eFrameworkPriorityLevel.LowLevel:
                        await _LowPriority.Writer.WriteAsync(request, combinedToken);
                        break;

                    case eFrameworkPriorityLevel.EndLevel:
                        await _inEndPriority.Writer.WriteAsync(request, combinedToken);
                        break;

                    case eFrameworkPriorityLevel.StartLevel:
                        await _inFirstPriority.Writer.WriteAsync(request, combinedToken);
                        break;
                }

                await request.jobTask;
            }
            catch (Exception ex)
            {
                Logger.logForThisTool($"Exception in RunAsync_Core: {ex}");
            }
            finally
            {
                linked?.Dispose();
            }
        }

        internal static async Task<T> RunSyncAsAsync_Core<T>(Func<T> syncFunc, CancellationToken? cancellationToken_Local = default)
        {
            RunTaskSchedulerEngine();
            if (syncFunc == null)
                throw new ArgumentNullException(nameof(syncFunc));

            return await RunAsync_Core<T>(
                async ct =>
                {
                    return syncFunc();
                },
                cancellationToken_Local);
        }

        internal static async Task RunSyncAsAsync_Core(Action syncAction, eFrameworkPriorityLevel level = eFrameworkPriorityLevel.MidLevel, CancellationToken? cancellationToken_Local = default)
        {
            RunTaskSchedulerEngine();
            if (syncAction == null)
                throw new ArgumentNullException(nameof(syncAction));

            _ = RunAsync_Core(
                ct =>
                {
                    //return Task.Run(syncAction, ct);
                    syncAction();
                    return Task.CompletedTask;
                },
                level,
                cancellationToken_Local);
        }

        /// <summary>
        /// Used In First Time For Run Engine Loop
        /// </summary>
        private static void RunTaskSchedulerEngine()
        {
            if (Interlocked.Exchange(ref FirstRun, 0) == 1)
            {
                _ = RunAsync_Core(async ct => {/*Don't Need AAny thing in this*/}, eFrameworkPriorityLevel.FrameworkLevel, default);
            }
        }

        private static bool AllQueuesEmpty()
        {
            return _FrameWorkPriority.Reader.Count == 0 &&
                   _inFirstPriority.Reader.Count == 0 &&
                   _highPriority.Reader.Count == 0 &&
                   _midPriority.Reader.Count == 0 &&
                   _LowPriority.Reader.Count == 0 &&
                   _inEndPriority.Reader.Count == 0;
        }

        internal static bool Shutdown_Immediate_Core()
        {
            _cts.Cancel();
            _FrameWorkPriority.Writer.TryComplete();
            _inFirstPriority.Writer.TryComplete();
            _highPriority.Writer.TryComplete();
            _midPriority.Writer.TryComplete();
            _LowPriority.Writer.TryComplete();
            _inEndPriority.Writer.TryComplete();
            return true;
        }

        internal static async Task<bool> Shutdown_WithRunedAsync_Core(int limitTime_sec)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            if (limitTime_sec > 0)
                cts.CancelAfter(TimeSpan.FromSeconds(limitTime_sec));
            while (!AllQueuesEmpty() || _activeJobs > 0 || !cts.IsCancellationRequested)
            {
                await Task.Delay(100); // 0.1Sec
            }
            _FrameWorkPriority.Writer.TryComplete();
            _inFirstPriority.Writer.TryComplete();
            _highPriority.Writer.TryComplete();
            _midPriority.Writer.TryComplete();
            _LowPriority.Writer.TryComplete();
            _inEndPriority.Writer.TryComplete();
            cts.Cancel();
            return true; // can Quit
        }

        internal static int GetRunningTaskCount_Core()
        {
            return Volatile.Read(ref _activeJobs);
        }

        /// <summary>
        /// set core logic setting for  delay time by ms.
        /// </summary>
        internal static void SetDelayTime_Core(int baseActiveDelayMs = 10, int baseIdleDelayMs = 50, int maxIdleDelayMs = 250)
        {
            if (baseIdleDelayMs < baseActiveDelayMs)
                baseIdleDelayMs = baseActiveDelayMs;
            BaseActiveDelayMs = Math.Clamp(baseActiveDelayMs, 1, 25);
            BaseIdleDelayMs = Math.Clamp(baseIdleDelayMs, 10, 50);
            MaxIdleDelayMs = Math.Clamp(maxIdleDelayMs, 100, 1000);
        }

        /// <summary>
        /// For Convert Usear Level To Framework Level
        /// </summary>
        /// <param name="PL"> User Level</param>
        /// <returns> Framework Level </returns>
        internal static eFrameworkPriorityLevel ConvertNormalToFrameworkPriorityLevel(ePriorityLevel PL)
        {
            switch (PL)
            {
                case ePriorityLevel.HighLevel:
                    return eFrameworkPriorityLevel.HighLevel;

                default:
                case ePriorityLevel.MidLevel:
                    return eFrameworkPriorityLevel.MidLevel;

                case ePriorityLevel.LowLevel:
                    return eFrameworkPriorityLevel.LowLevel;

                case ePriorityLevel.EndLevel:
                    return eFrameworkPriorityLevel.EndLevel;

                case ePriorityLevel.StartLevel:
                    return eFrameworkPriorityLevel.StartLevel;
            }
        }
    } // end of Folder_Ops class
} // end of NeraXTools name