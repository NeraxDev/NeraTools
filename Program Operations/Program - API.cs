// TODO: اینجا مشکل اساسی دارم در بخش ایسنک ها که متد متد های غیر ایسنک رو داخل ران ایسنک استفاده مینکیم ولی باید از تسک ران ایسینک بای سینک استفاده کنیم کوگر نه مشکل میخوریم چون ایمجا متد خای سینک نیستن و باید داخل ترید جدید اجرا شن

using NeraXTools.TaskManager;

namespace NeraXTools
{
    /// <summary>
    /// Provides high level API for process execution and termination operations.
    /// This class acts as public wrapper around ProgramOps_Core logic layer.
    /// </summary>
    public static class ProgramOps
    {
        #region ========================= RUN APIs Sync =========================

        /// <summary>
        /// Runs a single executable file.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <example>
        ///<![CDATA[
        /// ProgramOps.Run("app.exe");
        ///]]>
        ///</example>
        public static ProcessRunResult Run(string path)
            => ProgramOps_Core.RunWithPath_Core(new List<string> { path });

        /// <summary>
        /// Runs multiple executable files.
        /// </summary>
        /// <param name="paths">List of executable paths to run.</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run(new List<string> { "app1.exe", "app2.exe" });
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(List<string> paths)
            => ProgramOps_Core.RunWithPath_Core(paths);

        /// <summary>
        /// Runs a single executable file after a delay.
        /// </summary>
        /// <param name="path">Path to the executable file.</param>
        /// <param name="delaySeconds">Delay before starting (in seconds).</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run("app.exe", 5);
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(string path, int delaySeconds)
            => ProgramOps_Core.RunWithPath_Core(new List<string> { path }, delaySeconds);

        /// <summary>
        /// Runs multiple executable files after a delay.
        /// </summary>
        /// <param name="paths">List of executable paths to run.</param>
        /// <param name="delaySeconds">Delay before starting (in seconds).</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run(new List<string> { "app1.exe", "app2.exe" }, 10);
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(List<string> paths, int delaySeconds)
            => ProgramOps_Core.RunWithPath_Core(paths, delaySeconds);

        /// <summary>
        /// Runs the correct executable based on the current OS architecture.
        /// </summary>
        /// <param name="pathX64">Executable path for x64 systems.</param>
        /// <param name="pathX86">Executable path for x86 systems.</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run("app-x64.exe", "app-x86.exe");
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(string pathX64, string pathX86)
            => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 });

        /// <summary>
        /// Runs multiple executables based on the OS architecture.
        /// </summary>
        /// <param name="pathsX64">List of x64 executable paths.</param>
        /// <param name="pathsX86">List of x86 executable paths.</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run(new List<string>{"app64.exe"}, new List<string>{"app86.exe"});
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(List<string> pathsX64, List<string> pathsX86)
            => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86);

        /// <summary>
        /// Runs architecture-specific executable after a delay.
        /// </summary>
        /// <param name="pathX64">Executable path for x64 systems.</param>
        /// <param name="pathX86">Executable path for x86 systems.</param>
        /// <param name="delaySeconds">Delay before starting (in seconds).</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run("app-x64.exe", "app-x86.exe", 5);
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(string pathX64, string pathX86, int delaySeconds)
            => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 }, delaySeconds);

        /// <summary>
        /// Runs architecture-specific executables after a delay.
        /// </summary>
        /// <param name="pathsX64">List of x64 executable paths.</param>
        /// <param name="pathsX86">List of x86 executable paths.</param>
        /// <param name="delaySeconds">Delay before starting (in seconds).</param>
        /// <returns>ProcessRunResult with execution summary information.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.Run(new List<string>{"app64.exe"}, new List<string>{"app86.exe"}, 5);
        /// ]]>
        /// </example>
        public static ProcessRunResult Run(List<string> pathsX64, List<string> pathsX86, int delaySeconds)
            => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86, delaySeconds);

        #endregion ========================= RUN APIs Sync =========================

        #region ========================= RUN APIs Async =========================

        /// <summary>
        /// Runs a single executable file.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///    <![CDATA[
        ///  ProcessRunResult Result = await ProgramOps.RunAsync("app.exe" , YourCtsToken);
        ///
        ///    ]]>
        ///   </example>

        public static async Task<ProcessRunResult> RunAsync(string path, CancellationToken token = default)
           => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
             => ProgramOps_Core.RunWithPath_Core(new List<string> { path }), token);

        /// <summary>
        /// Runs a single executable file.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///    <![CDATA[
        /// ProgramOps.RunAsync("app.exe", PriorityLevel.startLevel, YourCtsToken);
        ///
        ///    ]]>
        ///    </example>

        public static void RunAsync(string path, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
           => _ = TaskSchedulerEngine.RunAsync(async ct
             => ProgramOps_Core.RunWithPath_Core(new List<string> { path }), PL, token);

        /// <summary>
        /// Runs multiple executable files.
        /// </summary>
        /// <param name="paths">List of executable paths.</param>
        /// <example>
        ///   <![CDATA[
        /// ProcessRunResult result = await ProgramOps.RunAsync(yourAppList , YourCtToken);
        ///
        ///     ]]>
        ///    </example>

        public static async Task<ProcessRunResult> RunAsync(List<string> paths, CancellationToken token = default)
          => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
              => ProgramOps_Core.RunWithPath_Core(paths), token);

        /// <summary>
        /// Runs multiple executable files.
        /// </summary>
        /// <param name="paths">List of executable paths.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.RunAsync(yourAppList, PriorityLevel.endlevel, YourCtsToken);
        ///
        ///  ]]>
        ///  </example>

        public static void RunAsync(List<string> paths, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
          => _ = TaskSchedulerEngine.RunAsync(async ct
              => ProgramOps_Core.RunWithPath_Core(paths), PL, token);

        /// <summary>
        /// Runs a single executable file after delay.
        /// </summary>
        /// <param name="path">Executable path.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.RunAsync("app.exe" , 2 , yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> RunAsync(string path, int delaySeconds, CancellationToken token = default)
           => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
              => ProgramOps_Core.RunWithPath_Core(new List<string> { path }, delaySeconds), token);

        /// <summary>
        /// Runs a single executable file after delay.
        /// </summary>
        /// <param name="path">Executable path.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.RunAsync(yourAppList , PriorityLevel.MidLevel , yourCtsToken);
        ///
        ///  ]]>
        ///  </example>

        public static void RunAsync(string path, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
           => _ = TaskSchedulerEngine.RunAsync(async ct
              => ProgramOps_Core.RunWithPath_Core(new List<string> { path }, delaySeconds), PL, token);

        /// <summary>
        /// Runs multiple executable files after delay.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result =  await ProgramOps.RunAsync(yourAppList, 2 , yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> RunAsync(List<string> paths, int delaySeconds, CancellationToken token = default)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.RunWithPath_Core(paths, delaySeconds), token);

        /// <summary>
        /// Runs multiple executable files after delay.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.RunAsync(yourAppList, 2 ,PriorityLevel.MidLevel, yourCtsToken);
        ///
        ///  ]]>
        ///  </example>

        public static void RunAsync(List<string> paths, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.RunWithPath_Core(paths, delaySeconds), PL, token);

        /// <summary>
        /// Runs executable based on OS architecture (x64 vs x86).
        /// </summary>
        /// <param name="pathX64">x64 executable path.</param>
        /// <param name="pathX86">x86 executable path.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await  ProgramOps.RunAsync(_x64.exe,_x86.exe , yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> RunAsync(string pathX64, string pathX86, CancellationToken token = default)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
               => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 }), token);

        /// <summary>
        /// Runs executable based on OS architecture (x64 vs x86).
        /// </summary>
        /// <param name="pathX64">x64 executable path.</param>
        /// <param name="pathX86">x86 executable path.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.RunAsync(_x64.exe,_x86.exe ,PriorityLevel.MidLevel, yourCtsToken);
        ///
        /// ]]>
        ///</example>

        public static void RunAsync(string pathX64, string pathX86, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => _ = TaskSchedulerEngine.RunAsync(async ct
               => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 }), PL, token);

        /// <summary>
        /// Runs multiple executables based on OS architecture.
        /// </summary>
        /// <param name="pathsX64">List of x64 executables.</param>
        /// <param name="pathsX86">List of x86 executables.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await  ProgramOps.RunAsync(YourList_x64,YourList_x86 , yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> RunAsync(List<string> pathsX64, List<string> pathsX86, CancellationToken token = default)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86), token);

        /// <summary>
        /// Runs multiple executables based on OS architecture.
        /// </summary>
        /// <param name="pathsX64">List of x64 executables.</param>
        /// <param name="pathsX86">List of x86 executables.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.RunAsync(YourList_x64,YourList_x86 ,PriorityLevel.MidLevel, yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static void RunAsync(List<string> pathsX64, List<string> pathsX86, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86), PL, token);

        /// <summary>
        /// Runs architecture specific executable after delay.
        /// </summary>
        /// <param name="pathX64">x64 executable path.</param>
        /// <param name="pathX86">x86 executable path.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.RunAsync(YourList_x64,YourList_x86, 2 , yourCtsToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> RunAsync(string pathX64, string pathX86, int delaySeconds, CancellationToken token = default)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 }, delaySeconds), token);

        /// <summary>
        /// Runs architecture specific executable after delay.
        /// </summary>
        /// <param name="pathX64">x64 executable path.</param>
        /// <param name="pathX86">x86 executable path.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.RunAsync(YourList_x64,YourList_x86, 2 ,  PriorityLevel.MidLevel, yourCtsToken);
        ///
        /// ]]>
        ///  </example>

        public static void RunAsync(string pathX64, string pathX86, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(new List<string> { pathX64 }, new List<string> { pathX86 }, delaySeconds), PL, token);

        /// <summary>
        /// Runs architecture specific executables after delay.
        /// </summary>
        /// <param name="pathsX64">x64 executable paths list.</param>
        /// <param name="pathsX86">x86 executable paths list.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.RunAsync(YourList_x64,YourList_x86, 2 , yourCtsToken);
        ///
        ///  ]]>
        ///  </example>

        public static async Task<ProcessRunResult> RunAsync(List<string> pathsX64, List<string> pathsX86, int delaySeconds, CancellationToken token = default)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86, delaySeconds), token);

        /// <summary>
        /// Runs architecture specific executables after delay.
        /// </summary>
        /// <param name="pathsX64">x64 executable paths list.</param>
        /// <param name="pathsX86">x86 executable paths list.</param>
        /// <param name="delaySeconds">Delay before execution (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.RunAsync(YourList_x64,YourList_x86, 2 , yourCtsToken);
        ///
        ///   ]]>
        ///   </example>

        public static void RunAsync(List<string> pathsX64, List<string> pathsX86, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.RunWithPathByOSBitType_Core(pathsX64, pathsX86, delaySeconds), PL, token);

        #endregion ========================= RUN APIs Async =========================

        #region ========================= TERMINATE APIs Sync =========================

        /// <summary>
        /// Terminates process by executable path.
        /// </summary>
        public static ProcessRunResult TerminateByPath(string path, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, 0, isJustNow);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        public static ProcessRunResult TerminateByPath(List<string> paths, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, null, paths, 0, isJustNow);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        public static ProcessRunResult TerminateByPath(string path, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, delaySeconds, isJustNow);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="paths">List of executable paths.</param>
        /// <param name="delaySeconds">Delay in seconds before termination.</param>
        /// <param name="isJustNow">If true, kills processes immediately; otherwise attempts graceful termination.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        public static ProcessRunResult TerminateByPath(List<string> paths, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, null, paths, delaySeconds, isJustNow);

        /// <summary>
        /// Terminates a process by executable name.
        /// </summary>
        /// <param name="Name">Executable process name.</param>
        /// <param name="isJustNow">If true, kills process immediately; otherwise attempts graceful termination.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByName("notepad.exe");
        /// ]]>
        /// </example>
        public static ProcessRunResult TerminateByName(string Name, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, new List<string> { Name }, null, 0, isJustNow);

        /// <summary>
        /// Terminates multiple processes by executable names.
        /// </summary>
        /// <param name="Names">List of process names.</param>
        /// <param name="isJustNow">If true, kills process immediately; otherwise attempts graceful termination.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByName(new List<string> { "notepad.exe", "calc.exe" });
        /// ]]>
        /// </example>
        public static ProcessRunResult TerminateByName(List<string> Names, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, Names, null, 0, isJustNow);

        /// <summary>
        /// Terminates single process by name after delay.
        /// </summary>
        /// <param name="Name">Process executable name.</param>
        /// <param name="delaySeconds">Delay in seconds.</param>
        /// <param name="isJustNow">If true, kills immediately.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByName("notepad.exe", 5);
        /// ]]>
        /// </example>
        public static ProcessRunResult TerminateByName(string Name, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, new List<string> { Name }, null, delaySeconds, isJustNow);

        /// <summary>
        /// Terminates multiple processes by names after delay.
        /// </summary>
        /// <param name="Names">List of process names.</param>
        /// <param name="delaySeconds">Delay in seconds.</param>
        /// <param name="isJustNow">If true, kills immediately.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByName(new List<string> { "notepad.exe", "calc.exe" }, 5);
        /// ]]>
        /// </example>
        public static ProcessRunResult TerminateByName(List<string> Names, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(null, Names, null, delaySeconds, isJustNow);

        /// <summary>
        /// Terminates a process by PID.
        /// </summary>
        /// <param name="PID">Process ID.</param>
        /// <param name="isJustNow">If true, kills immediately.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPID(1234);
        /// ]]>
        /// </example>
        public static ProcessRunResult TerminateByPID(int PID, bool isJustNow = false)
             => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, 0, isJustNow);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        public static ProcessRunResult TerminateByPID(List<int> PIDs, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(PIDs, null, null, 0, isJustNow);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        public static ProcessRunResult TerminateByPID(int PID, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, delaySeconds, isJustNow);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        public static ProcessRunResult TerminateByPID(List<int> PIDs, int delaySeconds, bool isJustNow = false)
            => ProgramOps_Core.Terminate_Core(PIDs, null, null, delaySeconds, isJustNow);

        #endregion ========================= TERMINATE APIs Sync =========================

        #region ========================= TERMINATE APIs Async =========================

        /// <summary>
        /// Terminates process by executable path.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppPath , YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static async Task<ProcessRunResult> TerminateByPathAsync(string path, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, 0, isJustNow), token);

        /// <summary>
        /// Terminates process by executable path.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppPath ,PriorityLevel.Midlevel, YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByPathAsync(string path, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, 0, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppList , YourCtToken);
        ///
        ///]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPathAsync(List<string> paths, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, null, paths, 0, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        ///   ProgramOps.TerminateByPathAsync(yourAppList , PriorityLevel.Midlevel, YourCtToken);
        ///  ]]>
        ///  </example>
        public static void TerminateByPathAsync(List<string> paths, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, null, paths, 0, isJustNow), PL, token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppPath , 10 , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPathAsync(string path, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///   <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppPath , 10 , PriorityLevel.Midlevel, YourCtToken);
        ///
        ///    ]]>
        ///    </example>

        public static void TerminateByPathAsync(string path, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, null, new List<string> { path }, delaySeconds, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppList , 10 , YourCtToken);
        ///
        ///   ]]>
        ///   </example>

        public static async Task<ProcessRunResult> TerminateByPathAsync(List<string> paths, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                 => ProgramOps_Core.Terminate_Core(null, null, paths, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="paths">Executable paths list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppList , 10 ,PriorityLevel.Midlevel, YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static void TerminateByPathAsync(List<string> paths, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                 => ProgramOps_Core.Terminate_Core(null, null, paths, delaySeconds, isJustNow), PL, token);

        /// <summary>
        /// Terminates process by executable name asynchronously.
        /// </summary>
        /// <param name="name">Executable process name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <param name="isJustNow">If true, terminates immediately.</param>
        /// <returns>ProcessRunResult with termination summary.</returns>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByNameAsync("notepad.exe", YourCtToken);
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByNameAsync(string name, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, new List<string> { name }, null, 0, isJustNow), token);

        /// <summary>
        /// Terminates process by executable name asynchronously with scheduling priority.
        /// </summary>
        /// <param name="name">Executable process name.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <param name="isJustNow">If true, terminates immediately.</param>
        /// <returns>None.</returns>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByNameAsync("notepad.exe", PriorityLevel.MidLevel, YourCtToken);
        /// ]]>
        /// </example>

        public static void TerminateByNameAsync(string name, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, new List<string> { name }, null, 0, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="names">Executable files Name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppNameList , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByNameAsync(List<string> names, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, names, null, 0, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="names">Executable files Name.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppNameList ,PriorityLevel.Midlevel, YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static void TerminateByNameAsync(List<string> names, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, names, null, 0, isJustNow), PL, token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="name">Executable file name.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppName , 5 , YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static async Task<ProcessRunResult> TerminateByNameAsync(string name, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(null, new List<string> { name }, null, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="name">Executable file name.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppName , 5 ,PriorityLevel.Midlevel, YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByNameAsync(string name, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(null, new List<string> { name }, null, delaySeconds, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="names">Executable names list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(yourAppName , 5 , YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static async Task<ProcessRunResult> TerminateByNameAsync(List<string> names, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                 => ProgramOps_Core.Terminate_Core(null, names, null, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="names">Executable names list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPathAsync(yourAppName , 5 ,PriorityLevel.Midlevel, YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByNameAsync(List<string> names, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                 => ProgramOps_Core.Terminate_Core(null, names, null, delaySeconds, isJustNow), PL, token);

        /// <summary>
        /// Terminates process by executable PID.
        /// </summary>
        /// <param name="PID">Executable file PID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPID , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPIDAsync(int PID, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, 0, isJustNow), token);

        /// <summary>
        /// Terminates process by executable PID.
        /// </summary>
        /// <param name="PID">Executable file PID.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPathAsync(appPID ,PriorityLevel.Midlevel, YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByPIDAsync(int PID, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, 0, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="PIDs">Executable file PIDs.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPID_List , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPIDAsync(List<int> PIDs, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(PIDs, null, null, 0, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes.
        /// </summary>
        /// <param name="PIDs">Executable file PIDs.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPID_List ,PriorityLevel.Midlevel, YourCtToken);
        ///
        ///  ]]>
        ///  </example>

        public static void TerminateByPIDAsync(List<int> PIDs, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
                => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(PIDs, null, null, 0, isJustNow), ePriorityLevel.StartLevel, token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="PID">Executable file PID.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        ///  <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPID , 5 , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPIDAsync(int PID, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates single process after delay.
        /// </summary>
        /// <param name="PID">Executable file PID.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProgramOps.TerminateByPathAsync(appPID , 5 ,PriorityLevel.Midlevel, YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByPIDAsync(int PID, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                => ProgramOps_Core.Terminate_Core(new List<int> { PID }, null, null, delaySeconds, isJustNow), PL, token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="PIDs">Executable PIDs list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPIDs_List , 5 , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static async Task<ProcessRunResult> TerminateByPIDAsync(List<int> PIDs, int delaySeconds, CancellationToken token = default, bool isJustNow = false)
            => await TaskSchedulerEngine.RunAsync<ProcessRunResult>(async ct
                 => ProgramOps_Core.Terminate_Core(PIDs, null, null, delaySeconds, isJustNow), token);

        /// <summary>
        /// Terminates multiple processes after delay.
        /// </summary>
        /// <param name="PIDs">Executable PIDs list.</param>
        /// <param name="delaySeconds">Delay before termination (seconds).</param>
        /// <param name="token">Cancellation token.</param>
        /// <param name="PL">Execution priority level inside scheduler engine.</param>
        /// <example>
        /// <![CDATA[
        /// ProcessRunResult result = await ProgramOps.TerminateByPathAsync(appPID , 5 , YourCtToken);
        ///
        /// ]]>
        /// </example>

        public static void TerminateByPIDAsync(List<int> PIDs, int delaySeconds, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, bool isJustNow = false)
            => _ = TaskSchedulerEngine.RunAsync(async ct
                 => ProgramOps_Core.Terminate_Core(PIDs, null, null, delaySeconds, isJustNow), PL, token);

        #endregion ========================= TERMINATE APIs Async =========================

        #region ========================= GetPID APIs Sync =========================

        /// <summary>
        /// Retrieves process IDs using process name.
        /// Lightweight synchronous wrapper around core PID discovery logic.
        /// Designed for simple and fast lookup scenarios where async scheduling is unnecessary.
        /// </summary>
        /// <param name="Name">Process executable name (for example, "notepad.exe").</param>
        /// <returns>List of process IDs that match the provided name.</returns>
        public static List<int> GetPID(string Name)
              => ProgramOps_Core.GetPIDs_Core(new List<string> { Name });

        /// <summary>
        /// Retrieves process IDs using multiple process names.
        /// Useful when searching for several targets simultaneously.
        /// </summary>
        /// <param name="Names">List of process executable names.</param>
        /// <returns>List of process IDs matching the provided names.</returns>
        public static List<int> GetPID(List<string> Names)
             => ProgramOps_Core.GetPIDs_Core(Names);

        /// <summary>
        /// Retrieves process IDs using both path and optional process name filtering.
        /// Provides more precise process discovery when multiple instances may exist.
        /// </summary>
        /// <param name="Path">Executable path to filter running processes.</param>
        /// <param name="Name">Optional executable name to filter.</param>
        /// <returns>List of process IDs matching the provided path and optional name.</returns>
        public static List<int> GetPID(string Path, string? Name = null)
              => ProgramOps_Core.GetPIDs_Core(new List<string> { Name }, new List<string> { Path });

        /// <summary>
        /// Retrieves process IDs using both multiple paths and optional name filters.
        /// Supports complex lookup scenarios across different executable locations.
        /// </summary>
        /// <param name="Paths">List of executable paths to search.</param>
        /// <param name="Names">Optional list of process names to filter by.</param>
        /// <returns>List of process IDs matching the provided paths and names.</returns>
        public static List<int> GetPID(List<string> Paths, List<string>? Names = null)
             => ProgramOps_Core.GetPIDs_Core(Names, Paths);

        #endregion ========================= GetPID APIs Sync =========================

        #region ========================= GetPID APIs Async =========================

        /// <summary>
        /// Asynchronously retrieves process IDs using process name with priority scheduling support.
        /// Execution is delegated to the scheduler engine to maintain controlled concurrency behavior.
        /// </summary>
        /// <param name="Name">Process executable name (e.g. "notepad.exe").</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning list of matching process IDs.</returns>
        public static async Task<List<int>> GetPIDAsync(string Name, CancellationToken token = default)
              => await TaskSchedulerEngine.RunAsync<List<int>>(async ct => ProgramOps_Core.GetPIDs_Core(new List<string> { Name }), token);

        /// <summary>
        /// Asynchronously retrieves process IDs using multiple process names.
        /// Suitable for high-performance scheduled lookup operations.
        /// </summary>
        /// <param name="Names">List of process executable names.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning list of matching process IDs.</returns>
        public static async Task<List<int>> GetPIDAsync(List<string> Names, CancellationToken token = default)
             => await TaskSchedulerEngine.RunAsync<List<int>>(async ct => ProgramOps_Core.GetPIDs_Core(Names), token);

        /// <summary>
        /// Asynchronously retrieves process IDs using path-based filtering with optional name matching.
        /// Useful for resolving processes when multiple executable variants exist.
        /// </summary>
        /// <param name="Path">Executable path to filter running processes.</param>
        /// <param name="Name">Optional process name filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning list of matching process IDs.</returns>
        public static async Task<List<int>> GetPIDAsync(string Path, string? Name = null, CancellationToken token = default)
              => await TaskSchedulerEngine.RunAsync<List<int>>(async ct => ProgramOps_Core.GetPIDs_Core(new List<string> { Name }, new List<string> { Path }), token);

        /// <summary>
        /// Asynchronously retrieves process IDs using path and name collections.
        /// Supports advanced lookup scenarios under scheduled execution control.
        /// </summary>
        /// <param name="Paths">Optional list of executable paths to search.</param>
        /// <param name="Names">Optional list of process names to filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning list of matching process IDs.</returns>
        public static async Task<List<int>> GetPIDAsync(List<string>? Paths, List<string>? Names = null, CancellationToken token = default)
             => await TaskSchedulerEngine.RunAsync<List<int>>(async ct => ProgramOps_Core.GetPIDs_Core(Names, Paths), token);

        #endregion ========================= GetPID APIs Async =========================

        #region ========================= AppExisted APIs Sync =========================

        /// <summary>
        /// Returns true if any process exists with the given executable name.
        /// </summary>
        /// <param name="Name">Process executable name (e.g. "notepad.exe").</param>
        /// <returns>True if process exists; otherwise false.</returns>
        public static bool isExitedByName(string Name) =>
              ProgramOps_Core.isExited_Core(new List<string> { Name });

        /// <summary>
        /// Returns true if any process exists with the given process ID.
        /// </summary>
        /// <param name="PID">Process ID.</param>
        /// <returns>True if process exists; otherwise false.</returns>
        public static bool isExitedByPID(int PID) =>
              ProgramOps_Core.isExited_Core(null, new List<int> { PID });

        /// <summary>
        /// Returns true if any process exists with the given executable path.
        /// </summary>
        /// <param name="Path">Executable file path.</param>
        /// <returns>True if process exists; otherwise false.</returns>
        public static bool isExitedByPath(string Path) =>
              ProgramOps_Core.isExited_Core(null, null, new List<string> { Path });

        #endregion ========================= AppExisted APIs Sync =========================

        #region ========================= AppExisted APIs Async =========================

        /// <summary>
        /// Asynchronously checks whether any process exists with the given executable name.
        /// </summary>
        /// <param name="Name">Process executable name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning true if process exists; otherwise false.</returns>
        public static async Task<bool> isExitedByNameAsync(string Name, CancellationToken token = default)
              => await TaskSchedulerEngine.RunSyncAsAsync<bool>(()
                  => ProgramOps_Core.isExited_Core(new List<string> { Name }), token);

        /// <summary>
        /// Asynchronously checks whether any process exists with the given process ID.
        /// </summary>
        /// <param name="pid">Process ID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning true if process exists; otherwise false.</returns>
        public static async Task<bool> isExitedByPIDAsync(int pid, CancellationToken token = default)
              => await TaskSchedulerEngine.RunSyncAsAsync<bool>(()
                  => ProgramOps_Core.isExited_Core(null, new List<int> { pid }), token);

        /// <summary>
        /// Asynchronously checks whether any process exists with the given executable path.
        /// </summary>
        /// <param name="path">Executable file path.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task returning true if process exists; otherwise false.</returns>
        public static async Task<bool> isExitedByPathAsync(string path, CancellationToken token = default)
              => await TaskSchedulerEngine.RunSyncAsAsync<bool>(()
                  => ProgramOps_Core.isExited_Core(null, null, new List<string> { path }), token);

        #endregion ========================= AppExisted APIs Async =========================

        #region ========================= AppGetProceses APIs Sync =========================

        // TODO: implement process listing wrappers in this region

        #endregion ========================= AppGetProceses APIs Sync =========================

        #region ========================= AppGetProceses APIs Async =========================

        // TODO: implement process listing wrappers in this region

        #endregion ========================= AppGetProceses APIs Async =========================
    }
}