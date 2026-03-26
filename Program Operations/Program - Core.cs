using System.Diagnostics;
using NeraXTools.LogManager;

namespace NeraXTools
{
    internal static partial class ProgramOps_Core
    {
        internal static ProcessRunResult RunWithPath_Core(List<string> paths, int time_sec = 0)
        {
            if (paths == null || paths.Count == 0)
            {
                Logger.logForThisTool("Please provide at least one path.");
                return new ProcessRunResult { Success = false };
            }

            int success = 0;
            int failed = 0;
            List<int> PIDs = new List<int>();

            foreach (var path in paths)
            {
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    Logger.logForThisTool($"{path} not found!");
                    failed++;
                    continue;
                }

                try
                {
                    if (time_sec > 0)
                        Thread.Sleep(TimeSpan.FromSeconds(time_sec));

                    Process process = Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                    if (process != null)
                    {
                        int pid = process.Id;
                        PIDs.Add(pid);
                    }
                    success++;
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool($"Error starting file:\n{ex}");
                    failed++;
                }
            }

            return new ProcessRunResult
            {
                Success = failed == 0,
                SuccessCount = success,
                FailedCount = failed,
                PIDs = PIDs
            };
        }

        internal static ProcessRunResult RunWithPathByOSBitType_Core(List<string> paths_x64 = null, List<string> paths_x86 = null, int time_sec = 0)
        {
            if ((paths_x64 == null || paths_x64.Count == 0) &&
                (paths_x86 == null || paths_x86.Count == 0))
            {
                Logger.logForThisTool("Please provide at least one file path list.");
                return new ProcessRunResult
                {
                    Success = false
                };
            }

            if (paths_x64 != null && paths_x86 != null &&
                paths_x64.Count != paths_x86.Count)
            {
                Logger.logForThisTool("Both x64 and x86 path lists must have equal count.");

                return new ProcessRunResult
                {
                    Success = false
                };
            }

            int maxCount = paths_x64?.Count ?? paths_x86.Count;

            int success = 0;
            int failed = 0;
            List<int> PIDs = new List<int>();

            for (int i = 0; i < maxCount; i++)
            {
                string selectedPath;

                if (Environment.Is64BitOperatingSystem)
                    selectedPath = paths_x64?[i] ?? paths_x86?[i];
                else
                    selectedPath = paths_x86?[i] ?? paths_x64?[i];

                if (string.IsNullOrWhiteSpace(selectedPath) || !File.Exists(selectedPath))
                {
                    Logger.logForThisTool($"{selectedPath} not found!");
                    failed++;
                    continue;
                }

                try
                {
                    if (time_sec > 0)
                        Thread.Sleep(TimeSpan.FromSeconds(time_sec));

                    Process process = Process.Start(new ProcessStartInfo
                    {
                        FileName = selectedPath,
                        UseShellExecute = true
                    });
                    if (process != null)
                    {
                        int pid = process.Id;
                        PIDs.Add(pid);
                    }
                    success++;
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool($"Error starting file:\n{ex}");
                    failed++;
                }
            }

            return new ProcessRunResult
            {
                Success = failed == 0,
                SuccessCount = success,
                FailedCount = failed,
                PIDs = PIDs
            };
        }

        internal static ProcessRunResult Terminate_Core(List<int> PIDs, List<string> Names = null, List<string> paths = null, int time_sec = 0, bool isJustNow = false)
        {
            if ((PIDs == null || PIDs.Count == 0) &&
                (Names == null || Names.Count == 0) &&
                (paths == null || paths.Count == 0))
            {
                Logger.logForThisTool("Please provide at least one identifier.");
                return new ProcessRunResult { Success = false };
            }

            List<int> list_PIDs = new List<int>();

            if (PIDs != null)
                list_PIDs = PIDs;

            if ((Names != null && Names.Count > 0) ||
                (paths != null && paths.Count > 0))
            {
                var found = ProgramOps.GetPID(paths, Names);

                if (found != null)
                    list_PIDs.AddRange(found);
            }

            list_PIDs = list_PIDs.Distinct().ToList();

            int success = 0;
            int failed = 0;

            foreach (var pid in list_PIDs)
            {
                try
                {
                    var process = Process.GetProcessById(pid);

                    if (!process.HasExited)
                    {
                        if (!isJustNow)
                            process.CloseMainWindow();

                        if (!process.WaitForExit(time_sec > 0 ? TimeSpan.FromSeconds(time_sec) : TimeSpan.FromSeconds(3)))
                            process.Kill(true);

                        success++;
                    }
                }
                catch
                {
                    failed++;
                    Logger.logForThisTool($"Fatal error terminating PID {pid}");
                }
            }

            return new ProcessRunResult
            {
                Success = failed == 0,
                SuccessCount = success,
                FailedCount = failed
            };
        }

        internal static List<int> GetPIDs_Core(List<string> Names = null, List<string> Paths = null)
        {
            List<int> result = new List<int>();

            if ((Names == null || Names.Count == 0) && (Paths == null || Paths.Count == 0))
            {
                Logger.logForThisTool("GetPidsAsync -> No search criteria provided.");
                return result;
            }

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    string processName = process.ProcessName;

                    string processPath = null;
                    try
                    {
                        processPath = process.ProcessName;
                    }
                    catch (Exception ex)
                    {
                        Logger.logForThisTool($"{ex}");
                    }

                    bool matched = false;

                    if (Names != null)
                    {
                        foreach (var name in Names)
                        {
                            if (string.IsNullOrWhiteSpace(name))
                                continue;

                            string cleanName =
                                Path.GetFileNameWithoutExtension(name);

                            if (string.Equals(processName,
                                cleanName,
                                StringComparison.OrdinalIgnoreCase))
                            {
                                matched = true;
                                break;
                            }
                        }
                    }

                    if (!matched && Paths != null)
                    {
                        foreach (var path in Paths)
                        {
                            if (string.IsNullOrWhiteSpace(path))
                                continue;

                            if (!string.IsNullOrEmpty(processPath) &&
                                string.Equals(processPath,
                                    path,
                                    StringComparison.OrdinalIgnoreCase))
                            {
                                matched = true;
                                break;
                            }
                        }
                    }

                    if (matched && !result.Contains(process.Id))
                    {
                        result.Add(process.Id);
                        Logger.logForThisTool($"Process found -> {process.ProcessName} | PID={process.Id}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool($"{ex}");
                }
            }

            if (result.Count == 0)
            {
                Logger.logForThisTool("GetPidsAsync -> No matching process found.");
            }
            else
            {
                Logger.logForThisTool($"GetPidsAsync -> Total {result.Count} process(es) found.");
            }

            return result;
        }

        internal static bool isExited_Core(List<string> Names = null, List<int> PIDs = null, List<string> Paths = null)
        {
            try
            {
                if ((PIDs == null || PIDs.Count == 0) &&
                   (Names == null || Names.Count == 0) &&
                   (Paths == null || Paths.Count == 0))
                {
                    Logger.logForThisTool("Please provide at least one identifier.");
                    return false;
                }
                List<int> list_PIDs = new List<int>();

                if (PIDs != null)
                    list_PIDs = PIDs;

                if ((Names != null && Names.Count > 0) ||
                    (Paths != null && Paths.Count > 0))
                {
                    var found = ProgramOps.GetPID(Paths, Names);

                    if (found != null)
                        list_PIDs.AddRange(found);
                }

                list_PIDs = list_PIDs.Distinct().ToList();

                Process? process;
                foreach (int pid in list_PIDs)
                {
                    process = Process.GetProcessById(pid);
                    if (!process.HasExited)
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.logForThisTool($"{ex}");
                return false;
            }
            return true;
        }
    }
}