using NeraTools.TaskManager.Apps;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using NeraTools.LogManager;

//using NeraTools.TaskManager.Apps;
//using NeraTools;

namespace NeraTools.TaskManager
{
    internal static class TaskMonitorConsoleCore
    {
        private static bool _isRunning = false;

        private static async Task SendAsync(string message)
        {
            using var server = new NamedPipeServerStream(
                "TaskMonoitor",
                PipeDirection.Out,
                1,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous
            );

            await server.WaitForConnectionAsync();

            byte[] buffer = Encoding.UTF8.GetBytes(message);

            //Convert message length to 4 bytes
            byte[] length = BitConverter.GetBytes(buffer.Length);

            //Send length first
            await server.WriteAsync(length, 0, length.Length);
            await Task.Delay(10);
            // Send message
            await server.WriteAsync(buffer, 0, buffer.Length);

            await server.FlushAsync();
        }

        private static DateTime _lastCpuCheck = DateTime.UtcNow;

        private static TimeSpan _lastTotalProcessorTime = TimeSpan.Zero;

        private static double GetProcessCpuUsage()
        {
            var currentProcess = Process.GetCurrentProcess();

            var now = DateTime.UtcNow;
            var cpuTime = currentProcess.TotalProcessorTime;

            double cpuUsedMs =
                (cpuTime - _lastTotalProcessorTime).TotalMilliseconds;

            double totalMs =
                (now - _lastCpuCheck).TotalMilliseconds *
                Environment.ProcessorCount;

            _lastCpuCheck = now;
            _lastTotalProcessorTime = cpuTime;

            if (totalMs == 0)
                return 0;

            return (cpuUsedMs / totalMs) * 100.0;
        }

        private static double GetProcessRamMB()
        {
            return Process.GetCurrentProcess().WorkingSet64 /
                   1024.0 / 1024.0;
        }

        private static ProcessRunResult? processRunResult = null; // for save Runed data

        internal static async Task TaskManitor_Core(eApplicationState state = eApplicationState.Running, int refreshRateSeconds = 1, CancellationToken token = default)
        {
            if (Environment.ProcessorCount <= 1)
            {
                Logger.logForThisTool("Warning: System performance is below recommended specifications ⚠");
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (state == eApplicationState.Running)
            {
                if (_isRunning)
                {
                    return;
                }
                processRunResult = ProgramOps.Run(Appslocations.TaskMonitorPanel_x64, Appslocations.TaskMonitorPanel_x86, 1);
                _isRunning = true;
                sb.AppendLine("Task Scheduler Monitor");
                sb.AppendLine($"Refreshing every {refreshRateSeconds} second...");
                sb.AppendLine("--------------------------");
                sb.AppendLine("Runing . . . . .");
                sb.AppendLine("--------------------------");

                await SendAsync(sb.ToString());
                await Task.Delay(5000); // 5 sec for Show Start Msg
                while (_isRunning && !token.IsCancellationRequested)
                {
                    double cpu = GetProcessCpuUsage();
                    double ram = GetProcessRamMB();
                    int count = TaskSchedulerEngine.GetRunningTaskCount();
                    await Task.Delay(refreshRateSeconds * 1000, token);
                    await SendAsync(
                        $"Thread used --> {count}\n" +
                        $"Core used --> {count / 2}\n" +
                        "------------------------------\n" +
                        $"CPU -> {cpu:F1}%\n" +
                        $"RAM -> {ram:F1} MB\n" +
                    "------------------------------"
                    );
#if DEBUG
                    //Console.WriteLine($"Debuging =====> {null}"); // only for debug
# endif
                }
            }
            else
            {
                sb.Clear();
                sb.AppendLine("--------------------------");
                sb.AppendLine("Closing  . . . . .");
                sb.AppendLine("--------------------------");
                _ = SendAsync(sb.ToString());
                if (_isRunning && processRunResult != null)
                    if (processRunResult.PIDs != null && processRunResult.PIDs.Count != 0)
                    {
                        ProgramOps.TerminateByPID(processRunResult.PIDs);
                    }
                _isRunning = false;
            }
        }
    }
}