using NeraXTools.TaskManager;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using NeraXTools.LogManager.Apps;

namespace NeraXTools.LogManager
{
    internal static partial class Logger_Core
    {
        internal static CancellationTokenSource globalCTS = new CancellationTokenSource();
        private static CancellationTokenSource timerCTS;
        internal static bool autooffline = true;
        private static int timeOut_sec = 15;
        private static NamedPipeServerStream server;
        private static List<LogDTO> uI_logs = new List<LogDTO>();
        private static List<LogDTO> console_logs = new List<LogDTO>();
        private static List<LogDTO> json_logs = new List<LogDTO>();
        private static List<LogDTO> txt_logs = new List<LogDTO>();
        private static readonly Queue<LogDTO> _logQueue = new();
        private static bool isRunedProcessLog = false;
        private static bool isServerInitialized = false;
        private static bool isUiAppRuned = false;
        private static ProcessRunResult processRunResult;

        internal static async Task Log_Core(
                                   string txt,
                                   eLogType type = eLogType.Info,
                                   eLogCategory category = eLogCategory.FrameworkLog,
                                   params eLogRecordMode[] mods)
        {
            try
            {
                if (globalCTS.IsCancellationRequested)
                {
                    globalCTS.Dispose();
                    globalCTS = new CancellationTokenSource();
                }
                if (Interlocked.CompareExchange(ref isRunedProcessLog, true, false) == false)
                    _ = TaskSchedulerEngine.RunAsync(async ct => { ProcessLog(timeOut_sec, timerCTS); }, ePriorityLevel.StartLevel, globalCTS.Token);
                if (timerCTS != null && !timerCTS.IsCancellationRequested)
                    timerCTS.Cancel();
                // اگر مود مشخص نشده، AllModes پیش‌فرض باشد
                var logMods = (mods == null || mods.Length == 0)
                    ? new[] { eLogRecordMode.AllModes }
                    : mods;
                // ایجاد یک instance جدید برای هر لاگ
                var newLog = new LogDTO
                {
                    msg = txt,
                    Type = type,
                    Category = category,
                    Mods = logMods
                };
                _logQueue.Enqueue(newLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logger Error => {ex}");// only for debug
            }
        }

        private static async Task ProcessLog(int timeOut_sec, CancellationTokenSource stopTimerCts)
        {
            try
            {
                while (!globalCTS.Token.IsCancellationRequested)
                {
                    LogDTO? log = null;

                    try
                    {
                        if (_logQueue.Count > 0)
                            log = _logQueue.Dequeue();
                    }
                    catch { }

                    if (log == null)
                    {
                        if (timerCTS != null && !timerCTS.IsCancellationRequested)
                        {
                            timerCTS.Cancel();
                            timerCTS.Dispose();
                            timerCTS = null;
                        }
                        if ((console_logs == null || console_logs.Count == 0) &&
                            (uI_logs == null || uI_logs.Count == 0) &&
                            (json_logs == null || json_logs.Count == 0) &&
                            (txt_logs == null || txt_logs.Count == 0))
                        {
                            stopTimerCts = new CancellationTokenSource();
                            if (autooffline)
                                _ = TimerForCTS(timeOut_sec, stopTimerCts);
                            else
                                stopTimerCts.Cancel();
                        }
                        await Task.Delay(100);
                        continue;
                    }
                    try
                    {
                        if (log.Mods.Contains(eLogRecordMode.Console) || log.Mods.Contains(eLogRecordMode.AllModes) /* only for debug*/)
                            console_logs.Add(log);

                        if (log.Mods.Contains(eLogRecordMode.UI) || log.Mods.Contains(eLogRecordMode.AllModes))
                            uI_logs.Add(log);

                        if (log.Mods.Contains(eLogRecordMode.Json) || log.Mods.Contains(eLogRecordMode.AllModes))
                            json_logs.Add(log);

                        if (log.Mods.Contains(eLogRecordMode.Txt) || log.Mods.Contains(eLogRecordMode.AllModes))
                            txt_logs.Add(log);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ProcessLog Worker Error => {ex}"); // only for debug
                    }
                    try
                    {
                        if (RunModsDTO.isConsole)
                            await ConsoleMode_Core(console_logs);

                        if (RunModsDTO.isUI)
                            await UIMode_Core(true, uI_logs);
                        else
                            await UIMode_Core(false);

                        if (RunModsDTO.isJson)
                            await jsonMode_Core(json_logs);

                        if (RunModsDTO.isTxt)
                            await txtMode_Core(txt_logs);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ProcessLog Worker Error => {ex}"); // only for debug
                    }
                }
            }
            finally
            {
                Interlocked.Exchange(ref isRunedProcessLog, false);
            }
        }

        private static async Task ConsoleMode_Core(List<LogDTO> logs)
        {
            if (logs == null)
                return;
            foreach (var log in logs)
            {
                if (log == null)
                    continue;
                if (log.Type == eLogType.Debug)
                {
#if !DEBUG
                    continue;
#endif
                }
                var typeColor = GetConsoleColor(log.Type);
                Console.ForegroundColor = typeColor;
                Console.WriteLine($"[{log.Type}]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{new string(' ', 7)}{log.msg}"); // 7 spaces
            }
            logs.Clear();
            Console.ResetColor();
        }

        private static async Task UIMode_Core(bool isEnable = true, List<LogDTO>? logs = null)
        {
            if (isEnable)
            {
                if (Interlocked.CompareExchange(ref isUiAppRuned, true, false) == false)
                    processRunResult = ProgramOps.Run(AppsLocations.TaskMonitorPanel_x64, AppsLocations.TaskMonitorPanel_x86);
                if (Interlocked.CompareExchange(ref isServerInitialized, true, false) == false)
                {
                    bool connected = await InitializeServer(10);
                    Interlocked.Exchange(ref isServerInitialized, connected);
                }
                if (logs == null)
                    return;
                foreach (var log in logs)
                {
                    if (log.Type == eLogType.Debug)
                    {
#if !DEBUG
                    continue;
#endif
                    }
                    if (log == null)
                        continue;
                    await SendAsync<LogDTO>(log);
                }
                logs.Clear();
            }
            else
            {
                if (Interlocked.CompareExchange(ref isUiAppRuned, false, true) == true)
                    ProgramOps.TerminateByPID(processRunResult.PIDs, true);
            }
        }

        // TODO: باید با  متد های خدم جای گزین شوند
        private static async Task jsonMode_Core(List<LogDTO> logs, string folderPath = null, string filename = "Log")
        {
            if (logs == null || logs.Count == 0)
                return;

            if (string.IsNullOrWhiteSpace(folderPath))
                folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(filename))
                _ = FolderOps.CreateFolder(folderPath);

            string nameWithoutExt = Path.GetFileNameWithoutExtension(filename);
            string fileName = nameWithoutExt + ".json";

            string fullPath = Path.Combine(folderPath, fileName);

            List<string> jsonLines = new List<string>();
            foreach (var log in logs)
            {
                if (log == null)
                    continue;

                if (log.Type == eLogType.Debug)
                {
#if !DEBUG
                    continue;
#endif
                }

                string json = JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = false });
                jsonLines.Add(json);
            }

            if (jsonLines.Count > 0)
                await File.AppendAllLinesAsync(fullPath, jsonLines);

            logs.Clear();
        }

        // TODO: باید با  متد های خدم جای گزین شوند
        private static async Task txtMode_Core(List<LogDTO> logs, string folderPath = null, string filename = "Log")
        {
            if (logs == null || logs.Count == 0)
                return;
            if (string.IsNullOrWhiteSpace(folderPath))
                folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(filename))
                FolderOps.CreateFolder(folderPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(filename);
            string fileName = nameWithoutExt + ".txt";
            string fullPath = Path.Combine(folderPath, fileName);
            List<string> textLines = new List<string>();
            foreach (var log in logs)
            {
                if (log == null)
                    continue;

                if (log.Type == eLogType.Debug)
                {
#if !DEBUG
                    continue;
#endif
                }
                string line = $"[{log.Type}] {log.msg}"; // فرض می‌کنیم LogDTO یک Message داره
                textLines.Add(line);
            }
            if (textLines.Count > 0)
                await File.AppendAllLinesAsync(fullPath, textLines, Encoding.UTF8);
            logs.Clear();
        }

        private static ConsoleColor GetConsoleColor(eLogType? type = eLogType.Info)
        {
            return type switch
            {
                eLogType.Debug => ConsoleColor.Gray,
                eLogType.Info => ConsoleColor.Cyan,
                eLogType.Warning => ConsoleColor.Yellow,
                eLogType.Error => ConsoleColor.Red,
                eLogType.Critical => ConsoleColor.DarkRed,
                eLogType.Exception => ConsoleColor.DarkRed,
                _ => ConsoleColor.White
            };
        }

        private static async Task<bool> InitializeServer(int timeOut_sec = 1)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeOut_sec)); //  after 10 sec
            try
            {
                server = new NamedPipeServerStream(
                     "LogMonoitor",
                     PipeDirection.Out,
                     1,
                     PipeTransmissionMode.Byte,
                     PipeOptions.Asynchronous
                 );
                await server.WaitForConnectionAsync(cts.Token);
                return true;
            }
            catch (OperationCanceledException) { return false; }
            catch (Exception ex) { return false; }
        }

        private static async Task SendAsync<T>(T msg)
        {
            if (server == null || !server.IsConnected || globalCTS.Token.IsCancellationRequested)
                return;
            try
            {
                string json = JsonSerializer.Serialize(msg);
                byte[] data = Encoding.UTF8.GetBytes(json);

                byte[] length = BitConverter.GetBytes(data.Length);
                await server.WriteAsync(length, 0, length.Length);

                await server.WriteAsync(data, 0, data.Length);
                await server.FlushAsync();
            }
            catch (Exception) { }
        }

        private static async Task TimerForCTS(int timeOut_sec, CancellationTokenSource stopTimer)
        {
            try
            {
                if (!globalCTS.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeOut_sec), stopTimer.Token);
                    globalCTS.Cancel();
                }
            }
            catch (TaskCanceledException) { }
        }
    }
}