namespace NeraTools.TaskManager
{
    /// <summary>
    /// Provides static methods for scheduling, executing, and managing asynchronous and synchronous tasks with
    /// configurable priority and concurrency settings. Offers APIs for task execution, thread pool configuration, delay
    /// tuning, and engine lifecycle control within the TaskManager execution environment.
    /// </summary>
    /// <remarks>All methods are thread-safe and designed for use in high-concurrency environments. Task
    /// execution order is influenced by specified priority levels, but strict sequencing is only guaranteed when
    /// returned Task objects are awaited. Fire-and-forget usage is supported; however, if tasks are not awaited,
    /// priority-based execution may not be strictly enforced. The engine supports both immediate and graceful shutdown,
    /// as well as runtime monitoring and adaptive performance scaling via thread and delay configuration.</remarks>
    public static partial class TaskSchedulerEngine
    {
        /// <summary>
        /// English:
        /// Runs an asynchronous task that returns a result.
        /// The task will be queued inside TaskManager execution engine
        /// and will execute according to specified priority level.
        ///
        /// Farsi:
        /// اجرای یک تسک ناهمزمان که مقدار خروجی برمی‌گرداند.
        /// تسک به صف داخلی TaskManager اضافه شده و بر اساس سطح اولویت اجرا می‌شود.
        /// </summary>
        /// <typeparam name="T">
        /// Type of return value from task.
        /// نوع خروجی تسک.
        /// </typeparam>
        /// <param name="taskFunc">
        /// Async function that receives cancellation token and returns result.
        /// تابع ناهمزمان که CancellationToken دریافت کرده و خروجی برمی‌گرداند.
        /// </param>
        /// <param name="PL">
        /// Priority level of task execution.
        /// تعیین سطح اولویت اجرای تسک.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token.
        /// Token اختیاری برای لغو عملیات.
        /// </param>
        /// <returns>Task result value</returns>
        /// <example>
        /// <![CDATA[
        /// int result = await TaskSchedulerEngine.RunAsync<int>(
        ///     async token =>
        ///     {
        ///         await Task.Delay(1000, token);
        ///         return 10;
        ///     },
        ///     PriorityLevel.HighLevel);
        /// ]]>
        /// </example>
        public static async Task<T> RunAsync<T>(Func<CancellationToken, Task<T>> taskFunc, CancellationToken? cancellationToken = default)
            => await TaskSchedulerEngine_Core.RunAsync_Core<T>(taskFunc, cancellationToken);

        /// <summary>
        /// English:
        /// Runs an asynchronous task without returning result.
        /// Used for background processing or fire-and-forget operations.
        ///
        /// Farsi:
        /// اجرای تسک ناهمزمان بدون مقدار خروجی.
        /// برای عملیات‌های پس‌زمینه استفاده می‌شود.
        /// </summary>
        /// <param name="taskFunc">Async task function</param>
        /// <param name="PL">Priority level</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <example>
        /// <![CDATA[
        /// _= TaskSchedulerEngine.RunAsync(
        ///     async ct =>
        ///     {
        ///         await Task.Delay(2000, ct);
        ///     },
        ///     PriorityLevel.MidLevel,
        ///     token);
        /// ]]>
        /// </example>
        /// /// <remarks>
        /// English:
        /// Fire-and-forget execution is supported.
        /// If the returned Task is not awaited, execution priority level ordering
        /// may not be strictly guaranteed.
        ///
        /// Tasks may execute individually without strict priority sequencing
        /// if the caller does not observe the returned Task.
        ///
        /// ----------------------------------------------------------------
        /// Farsi:
        /// اجرای Fire-and-forget پشتیبانی می‌شود.
        /// اگر Task برگشتی await نشود، ترتیب اجرای بر اساس سطح اولویت
        /// به صورت قطعی تضمین نمی‌شود.
        ///
        /// در صورت عدم استفاده از await، ممکن است تسک‌ها به صورت تک‌به‌تک
        /// و بدون رعایت کامل ترتیب اولویت اجرا شوند.
        /// </remarks>
        public static Task RunAsync(Func<CancellationToken, Task> taskFunc, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken cancellationToken = default)
            => _ = TaskSchedulerEngine_Core.RunAsync_Core(taskFunc, TaskSchedulerEngine_Core.ConvertNormalToFrameworkPriorityLevel(PL), cancellationToken);

        /// <summary>
        /// English:
        /// Converts a synchronous function into an asynchronous scheduled task.
        /// The task will be queued inside TaskSchedulerEngine execution pipeline.
        /// If the operation is CPU-bound, it will be executed using ThreadPool workers.
        ///
        /// Farsi:
        /// تبدیل تابع سینک به تسک ناهمزمان و ارسال آن به موتور زمان‌بندی TaskSchedulerEngine.
        /// اگر عملیات از نوع CPU-bound باشد، در ThreadPool اجرا خواهد شد.
        ///
        /// ----------------------------------------------------------------
        /// Parameters Explanation | توضیح پارامترها
        /// ----------------------------------------------------------------
        /// syncFunc:
        ///     Synchronous function without CancellationToken parameter.
        ///     تابع سینک که هیچ Token لغو عملیات دریافت نمی‌کند.
        ///
        /// PL (PriorityLevel):
        ///     Defines execution priority inside scheduler queue.
        ///     تعیین سطح اولویت اجرا در صف زمان‌بندی.
        ///
        /// cancellationToken:
        ///     Optional token for canceling execution.
        ///     Token اختیاری برای لغو عملیات.
        ///
        /// ----------------------------------------------------------------
        /// Example | مثال استفاده
        /// ----------------------------------------------------------------
        /// <![CDATA[
        /// int result = await TaskSchedulerEngine.RunSyncAsAsync<int>(
        ///     () =>
        ///     {
        ///         int a = 10;
        ///         int b = 20;
        ///         return a + b;
        ///     },
        ///     PriorityLevel.HighLevel);
        /// ]]>
        /// </summary>
        /// <typeparam name="T">
        /// Return type of function.
        /// نوع خروجی تابع.
        /// </typeparam>
        public static async Task<T> RunSyncAsAsync<T>(Func<T> syncFunc, CancellationToken? cancellationToken = default)
            => await TaskSchedulerEngine_Core.RunSyncAsAsync_Core<T>(syncFunc, cancellationToken);

        /// <summary>
        /// English:
        /// Converts synchronous Action into asynchronous scheduled task.
        /// Used for background operations that do not return values.
        ///
        /// Farsi:
        /// تبدیل اکشن سینک به تسک ناهمزمان.
        /// برای عملیات پس‌زمینه که خروجی ندارند استفاده می‌شود.
        ///
        /// ----------------------------------------------------------------
        /// Example | مثال استفاده
        /// ----------------------------------------------------------------
        /// <![CDATA[
        ///  TaskSchedulerEngine.RunSyncAsAsync(
        ///     () =>
        ///     {
        ///         Console.WriteLine("Background logging operation");
        ///     },
        ///     PriorityLevel.LowLevel,
        ///     token);
        /// ]]>
        /// </summary>
        /// /// <remarks>
        /// English:
        /// Fire-and-forget execution is supported.
        /// If the returned Task is not awaited, execution priority level ordering
        /// may not be strictly guaranteed.
        ///
        /// Tasks may execute individually without strict priority sequencing
        /// if the caller does not observe the returned Task.
        ///
        /// ----------------------------------------------------------------
        /// Farsi:
        /// اجرای Fire-and-forget پشتیبانی می‌شود.
        /// اگر Task برگشتی await نشود، ترتیب اجرای بر اساس سطح اولویت
        /// به صورت قطعی تضمین نمی‌شود.
        ///
        /// در صورت عدم استفاده از await، ممکن است تسک‌ها به صورت تک‌به‌تک
        /// و بدون رعایت کامل ترتیب اولویت اجرا شوند.
        /// </remarks>
        public static Task RunSyncAsAsync(Action syncAction, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken? cancellationToken = default)
            => _ = TaskSchedulerEngine_Core.RunSyncAsAsync_Core(syncAction, TaskSchedulerEngine_Core.ConvertNormalToFrameworkPriorityLevel(PL), cancellationToken);

        /// <summary>
        /// English:
        /// Configures thread pool usage based on percentage of CPU resources.
        /// Useful for adaptive performance scaling.
        ///
        /// Farsi:
        /// تنظیم میزان استفاده از پردازنده بر اساس درصد منابع CPU.
        /// مناسب برای کنترل خودکار عملکرد سیستم.
        /// </summary>
        /// <param name="percent">
        /// CPU usage percentage.
        /// درصد استفاده از CPU.
        /// </param>
        /// <example>
        /// <![CDATA[
        /// TaskSchedulerEngine.SetThreadsCountByPercen(
        ///     ThreadUsagePercent.SixtyPercent);
        /// ]]>
        /// </example>
        public static void SetThreadsCountByPercen(eThreadUsagePercent percent)
            => TaskSchedulerEngine_Core.SetThreadsCount_ByPercent_Core(percent);

        /// <summary>
        /// English:
        /// Configures worker thread count based on CPU cores.
        /// Each core will typically map to two worker threads.
        ///
        /// Farsi:
        /// تنظیم تعداد رشته‌های اجرایی بر اساس تعداد هسته‌های پردازنده.
        /// معمولاً هر هسته برابر با دو رشته کاری در نظر گرفته می‌شود.
        /// </summary>
        /// <param name="coreCount">Number of cores to use</param>
        /// <example>
        /// <![CDATA[
        /// TaskSchedulerEngine.SetThreadsCountByCore(4);
        /// ]]>
        /// </example>
        public static void SetThreadsCountByCore(int coreCount)
            => TaskSchedulerEngine_Core.SetThreadsCount_ByCore_Core(coreCount);

        /// <summary>
        /// English:
        /// Configures exact number of worker threads.
        /// Gives direct control over concurrency level.
        /// Best used at the end of programs when closing.
        /// Farsi:
        /// تنظیم دقیق تعداد رشته‌های اجرایی.
        /// کنترل مستقیم بر میزان همزمانی عملیات.
        /// بهتر در اخر برنامه ها هنگام بسته شدن استفاده شود
        /// </summary>
        /// <param name="threadCount">Exact worker thread count</param>
        /// <example>
        /// <![CDATA[
        /// TaskSchedulerEngine.SetThreadsCountByThreads(8);
        /// ]]>
        /// </example>
        public static void SetThreadsCountByThreads(int threadCount)
            => TaskSchedulerEngine_Core.SetThreadsCount_ByThreads_Core(threadCount);

        /// <summary>
        /// English:
        /// Immediately shuts down Task execution engine.
        /// Cancels all tasks and stops processing queues instantly.
        ///
        /// Farsi:
        /// خاموش کردن فوری موتور اجرای تسک‌ها.
        /// لغو فوری تمام تسک‌ها و صف‌ها.
        /// </summary>
        public static bool ShutdownNeraToolImmediate()
            => TaskSchedulerEngine_Core.Shutdown_Immediate_Core();

        /// <summary>
        /// English:
        /// Gracefully shuts down the Task execution engine.
        /// Waits for all running and queued tasks to complete before shutdown.
        /// Recommended usage is at the end of programs when closing.
        ///
        /// Farsi:
        /// خاموش کردن امن و تدریجی موتور اجرای تسک‌ها.
        /// صبر می‌کند تا تمام تسک‌های در حال اجرا و صف‌ها تمام شوند.
        /// بهتر است در انتهای برنامه‌ها هنگام بسته شدن استفاده شود.
        /// مراقب باشید: زمان محدود ورودی ممکن است باعث کنسل شدن برخی عملیات سریع شود.
        /// اگر می‌خواهید همه تسک‌ها حتماً کامل شوند، 0 را وارد کنید.
        /// </summary>
        /// <param name="limitTime_sec">
        /// Wait time in seconds. Default is 0 (No limit, waits until all tasks complete).
        /// زمان انتظار به ثانیه. مقدار پیش‌فرض 0 است (بدون محدودیت، تا پایان همه تسک‌ها صبر می‌کند).
        /// </param>
        /// <returns>
        /// True if shutdown completed successfully.
        /// در صورت اتمام موفقیت‌آمیز خاموش شدن، True برمی‌گرداند.
        /// </returns>
        public static async Task<bool> ShutdownNeraToolGracefulAsync(int limitTime_sec = 0)
            => await TaskSchedulerEngine_Core.Shutdown_WithRunedAsync_Core(limitTime_sec);

        /// <summary>
        /// Gets the number of currently running tasks inside TaskManager.
        /// </summary>
        /// <returns>Running task count.</returns>
        public static int GetRunningTaskCount()
            => TaskSchedulerEngine_Core.GetRunningTaskCount_Core();

        /// <summary>
        /// Starts or stops the task monitor console display.
        /// </summary>
        /// <param name="enable">
        /// Set to <c>true</c> to start the monitor;
        /// set to <c>false</c> to stop it.
        /// </param>
        /// <param name="delayTime">
        /// (Optional) The delay interval between updates in milliseconds.
        /// Only used when <paramref name="enable"/> is <c>true</c>.
        /// </param>
        /// <param name="cancellationToken">
        /// (Optional) A token to cancel the monitoring operation.
        /// </param>
        /// <example>
        /// <![CDATA[
        /// Start monitoring:
        /// TaskSchedulerEngine.TaskMonitor();
        /// TaskSchedulerEngine.TaskMonitor(true, delayTime, token);
        ///
        /// Stop monitoring:
        /// TaskSchedulerEngine.TaskMonitor(false);
        /// ]]>
        /// </example>
        public static void TaskMonitor(bool isEnable = true, int refreshRateSeconds = 1, CancellationToken token = default)
              => _ = isEnable ? TaskMonitorConsoleCore.TaskManitor_Core(eApplicationState.Running, refreshRateSeconds, token) : TaskMonitorConsoleCore.TaskManitor_Core(eApplicationState.Disabled, refreshRateSeconds, token);

        /// <summary>
        /// Set scheduler delay time using milliseconds unit.
        /// تنظیم زمان تأخیر Scheduler با واحد میلی‌ثانیه.
        /// </summary>
        /// <param name="activeMs">
        /// Delay base value when scheduler is actively executing jobs.
        /// Base delay for CPU pacing during high workload execution.
        /// مقدار پایه تأخیر زمانی در حالت اجرای فعال وظایف.
        /// </param>
        /// <param name="idleMs">
        /// Base delay value when system is idle.
        /// Used to reduce CPU polling and unnecessary loop spinning.
        /// مقدار پایه تأخیر زمانی در حالت بیکاری سیستم.
        /// </param>
        /// <param name="maxIdleMs">
        /// Maximum allowed delay ceiling during idle state.
        /// Prevents excessive latency and responsiveness loss.
        /// سقف بیشینه تأخیر در حالت بیکاری.
        /// </param>
        public static void SetDelayTimeMilliseconds(int activeMs = 10, int idleMs = 50, int maxIdleMs = 250)
            => TaskSchedulerEngine_Core.SetDelayTime_Core(activeMs, idleMs, maxIdleMs);

        /// <summary>
        /// Set scheduler delay time using seconds unit.
        /// تنظیم زمان تأخیر Scheduler با واحد ثانیه.
        /// </summary>
        /// <param name="activeSec">Active execution delay in seconds.</param>
        /// <param name="idleSec">Idle state delay in seconds.</param>
        /// <param name="maxIdleSec">Maximum idle delay in seconds.</param>
        public static void SetDelayTimeSeconds(int activeSec = 0, int idleSec = 0, int maxIdleSec = 0)
            => TaskSchedulerEngine_Core.SetDelayTime_Core(activeSec * 1000, idleSec * 1000, maxIdleSec * 1000);
    }
}