NeraTools

NeraTools is a lightweight, evolving collection of high-performance utilities for system management, logging, and async task handling in .NET.

It is designed to cover common developer needs and provides tools that are not available by default in .NET.

🚀 Features
System and Process Management – Tools for monitoring, controlling, and managing processes and system operations.

Logging and Operation Management – Track and manage events and system operations for easier debugging and analysis.

Async Task Queuing and Execution – Full async task management with queuing and fine-grained execution control, similar to the built-in Async system but with more flexibility.

General Developer Utilities – A collection of useful helper functions for everyday programming tasks that are not natively provided in .NET.

Lightweight and Optimized – Designed to minimize resource usage, suitable for scalable and scheduled applications.

Compatible with .NET 10 – Optimized for performance and fully compatible with the latest .NET versions.


- 📦 Installation

Clone the Repository

git clone https://github.com/NeraxDev/NeraTools.git


Download Releases


- **Full package (DLL + tools)**  
[NeraTools v0.1.0 ZIP](https://github.com/NeraxDev/NeraTools/releases/download/v0.1.0/NeraTools.v0.1.0.zip)

- **DLL only (not recommended, no extra tools)**  
[NeraTools DLL](https://github.com/NeraxDev/NeraTools/releases/download/v0.1.0/NeraTools_notRecommend.dll)

- **Source code**
[ZIP](https://github.com/NeraxDev/NeraTools/archive/refs/tags/v0.1.0.zip)  | [TAR.GZ](https://github.com/NeraxDev/NeraTools/archive/refs/tags/v0.1.0.tar.gz)


Clone for latest development

git clone https://github.com/NeraxDev/NeraTools.git

NuGet --> Coming soon…

🧠 Usage
Example: Get Process IDs
var processIds = await ProcessTools.GetProcessIdsAsync(new[] { "chrome", "notepad" });

foreach (var id in processIds)
{
    Console.WriteLine(id);
}

This is just a simple example. Other tools, including logging and async task management, are already included, and more general-purpose utilities will be added over time.

⚙️ Why NeraTools?

Unlike basic libraries, NeraTools focuses on:

High performance and system management
Logging and operation management
Async task queuing and safe execution
Useful general-purpose developer utilities
📌 Roadmap
Add more system utilities
Improve current performance
Add unit tests
Expand documentation
Prepare NuGet package
🤝 Contributing

This project is still evolving, and more tools will be added over time.

If you are an interested developer and want to help improve NeraTools, your contributions are very welcome. You can submit pull requests or open an issue to suggest improvements or new features. Every contribution helps make NeraTools better for everyone. 
Thanks 💞

📜 License

Licensed under Apache License 2.0.
پ





## ProgramOps (Process execution & management)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) | Example (see below) |
|---|---|---|---|---|
| `Run`, `RunAsync` | Run executables (single/multiple), optional delay and architecture variants (x64/x86). | اجرای برنامه‌ها با تأخیر و نسخهٔ معماری. | `Program Operations/Execte Program - API.cs`, `Execte Program - Core.cs` | [Example](#poe-run) [#POE1] |
| `TerminateByPath`, `TerminateByName`, `TerminateByPID` (sync & async) | Terminate processes by path, name or PID. | خاتمه فرایندها بر اساس مسیر، نام یا PID. | `Program Operations/Execte Program - API.cs` | [Example](#poe-term) [#POE6] |
| `GetPID`, `GetPIDAsync`, `isExitedByName*` | Query running processes and check existence. | یافتن PIDها و بررسی وجود فرایندها. | `Program Operations/Execte Program - API.cs` | [Example](#poe-getpid) [#POE8] |

---

## TaskSchedulerEngine / TaskManager (Task wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) | Example (see below) |
|---|---|---|---|---|
| `RunAsync<T>(Func<CancellationToken, Task<T>>)` | Runs an async task that returns a result; scheduled with priority. | اجرای تسک ناهمگام با مقدار خروجی و زمان‌بندی براساس اولویت. | `TaskManager/TaskManager - API.cs` | [Example ](#tm-runasync-t) [#TME1] |
| `RunAsync(Func<CancellationToken, Task>)` | Runs an async task without result (fire-and-forget or awaited). | اجرای تسک ناهمگام بدون خروجی (Fire-and-forget یا با await). | `TaskManager/TaskManager - API.cs` | [Example](#tm-runasync)  [#TME2] |
| `RunSyncAsAsync<T>(Func<T>)` | Converts a synchronous function into a scheduled async task returning a value. | تبدیل تابع همگام به تسک ناهمگام و بازگرداندن مقدار. | `TaskManager/TaskManager - API.cs` | [Example](#tm-runsyncasasync-t)  [#TME3]|
| `RunSyncAsAsync(Action)` | Converts a synchronous Action into a scheduled async task (no result). | تبدیل اکشن همگام به تسک ناهمگام بدون خروجی. | `TaskManager/TaskManager - API.cs` | [Example](#tm-runsyncasasync) [#TME4]|
| `SetThreadsCountByPercen(eThreadUsagePercent)` | Configure thread pool usage based on CPU percentage for adaptive scaling. | تعیین میزان استفاده از رشته‌ها بر اساس درصد مصرف CPU. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bypercent)  [#TME5]|
| `SetThreadsCountByCore(int)` | Set worker thread count based on CPU core count. | تنظیم تعداد رشته‌ها بر اساس تعداد هسته‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bycore)  [#TME6] |
| `SetThreadsCountByThreads(int)` | Set exact number of worker threads. | تنظیم دقیق تعداد رشته‌های کاری. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bythreads) [#TME7] | 
| `ShutdownNeraToolImmediate()` | Immediately cancel all tasks and stop the engine. | خاموش‌سازی فوری موتور و کنسل‌کردن همه تسک‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-shutdown-immediate) [#TME8]|
| `ShutdownNeraToolGracefulAsync(int)` | Graceful shutdown: wait for running/queued tasks to finish (optional timeout). | خاموش‌سازی تدریجی با امکان تعیین زمان انتظار برای اتمام تسک‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-shutdown-graceful) [#TME9]|
| `GetRunningTaskCount()` | Returns number of currently running tasks. | تعداد تسک‌های درحال اجرا را برمی‌گرداند. | `TaskManager/TaskManager - API.cs` | [Example](#tm-getrunningcount) [#TME10] |
| `TaskMonitor(bool, int, CancellationToken)` | Start/stop console task monitor with refresh interval. | شروع/توقف نمایشگر کنسول تسک‌ها با نرخ به‌روزرسانی. | `TaskManager/TaskManager - API.cs` | [Example](#tm-taskmonitor)  [#TME11] |
| `SetDelayTimeMilliseconds(int activeMs, int idleMs, int maxIdleMs)` | Configure scheduler delays in milliseconds for active/idle states. | تنظیم تاخیر زمان‌بندی‌کننده به میلی‌ثانیه برای حالات فعال و بیکاری. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setdelay-ms) [#TME12]|
| `SetDelayTimeSeconds(int activeSec, int idleSec, int maxIdleSec)` | Configure scheduler delays in seconds (wrapper over ms method). | تنظیم تاخیر زمان‌بندی‌کننده به ثانیه (شبه‌رپ) بر پایه متد میلی‌ثانیه. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setdelay-sec) [#TME13]|

---

 
## Logger / LogManager (Logging wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) | Example (see below) |
|---|---|---|---|---|
| `log` | Log a message to the user's application log (category: UsearApplicationLog). | ثبت پیام در لاگ برنامهٔ کاربر (دسته: UsearApplicationLog). | `LogManager/Logging - API.cs` | [Example](#lm-log) [#LME1] |
| `logForThisTool` | Log a framework/internal message (category: FrameworkLog). | ثبت پیام داخلی/فریم‌ورک (دسته: FrameworkLog). | `LogManager/Logging - API.cs` | [Example](#lm-logForThisTool) [#LME2] |
| `writeLogInConsole(bool isEnable = true)` | Enable/disable console logging. | فعال/غیرفعال‌سازی لاگ در کنسول. | `LogManager/Logging - API.cs` | [Example](#lm-writeConsole) [#LME3] |
| `writeLogInUi(bool isEnable = true)` | Enable/disable UI logging (for UI panels). | فعال/غیرفعال‌سازی لاگ در رابط کاربری (UI). | `LogManager/Logging - API.cs` | [Example](#lm-writeUi) [#LME4] |
| `writeLogInJsonFile(bool isEnable = true, string saveLocation = null, string fileName = "Log")` | Enable JSON file logging and set location/name. | فعال‌سازی لاگ در فایل JSON و تعیین مسیر/نام فایل. | `LogManager/Logging - API.cs` | [Example](#lm-writeJson) [#LME5] |
| `writeLogInTextFile(bool isEnable = true, string saveLocation = null, string fileName = "Log")` | Enable text file logging and set location/name. | فعال‌سازی لاگ در فایل متنی و تعیین مسیر/نام فایل. | `LogManager/Logging - API.cs` | [Example](#lm-writeText) [#LME6] |
| `AutoOfflinetion(bool isEnable = true)` | Enable/disable automatic offline mode (default: enabled). | فعال/غیرفعال‌سازی حالت آفلاین خودکار (پیش‌فرض: فعال). | `LogManager/Logging - API.cs` | [Example](#lm-autoOffline) [#LME7] |
| `DisableLoggerSystem()` | Immediately disable logger system and cancel internal operations. | غیرفعال‌سازی فوری سیستم لاگر و کنسل‌کردن عملیات داخلی. | `LogManager/Logging - API.cs` | [Example](#lm-disable) [#LME8] |

---
---

## FolderOps (Folder operations wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) |
|---|---|---|---|
| `CreateFolder*` (sync & async) | Create single or multiple folders. | ساخت فولدر تک یا چندتایی. | `Folder Operations/Create Folder/Create Folder -API.cs`, `MakeFolder - Core.cs` |
| `CopyFolder*`, `CopyFolders*` (sync & async) | Copy folders with filters (name/ext/size/date/attributes) and options. | کپی فولدرها با فیلترها و گزینه‌ها (بازنویسی، بکاپ، ...). | `Folder Operations/FileTransfer/Copy Folder/Folder Copy - API .cs`, `Folder Copy - CoreAPI.cs` |
| `MoveFolder*`, `MoveFolders*` (sync & async) | Move (cut) folders; same filter/options as copy. | جابجایی (کات) فولدرها با فیلترها و گزینه‌ها. | `Folder Operations/FileTransfer/Move Folder/Move Folder - API.cs`, `Move Folder - CoreAPI.cs` |
| `DeleteFolder*`, `DeleteFolders*` | Delete folders with options (backup, retry, recursive, logging). | حذف فولدرها با گزینه‌هایی مثل بکاپ، تکرار، حذف بازگشتی و لاگ. | `Folder Operations/Delete Folder/Delete Folder - API.cs`, `Delete Folder - CoreAPI.cs` |
| `FolderTransfomOptions`, `FolderDeleteOptions` (enums) | Flags to control copy/move/delete behavior. | enumهای کنترل رفتار عملیات پوشه. | `Folder Operations/Enums/Enum_API.cs`, `Enums_Core.cs` |

---

## FileOps (File operations wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) |
|---|---|---|---|
| `CreateFileFullPath`, `CreateFile`, `CreateFileFullPath_Async`, `CreateFile_Async` | Create files from full paths or directory + name. | ایجاد فایل از مسیر کامل یا مسیر+نام (همگام و ناهمگام). | `File Operations/File Operations - API.cs`, `File Operations/File Operations - Core.cs` |

---
## Other notable files & helpers

- `___InDebugTime/__Debug__.cs` — debug helpers used during development.
- `AssemblyInfo.cs` and `obj/...` — build metadata and generated files.

---

## How to build and run

1. Restore and build solution (from repository root):

```powershell
dotnet restore
dotnet build
```		

2. Run a sample / console debug app:

```powershell
dotnet run --project ..\NeraTool_ConsoleApp_DebugingServise\NeraTool_ConsoleApp_DebugingServise.csproj
```

3. For WPF panels open the solution in Visual Studio 2026 and set the desired startup project (e.g., `LogMonitorPanal`, `TaskMonitorPanal`, or `NeraTool_WPF_DebugingServise`).

---

If you want, I can expand this single README into per-project README files with example code snippets (English + Persian) for each wrapper. Reply with: `Create per-project READMEs` to proceed.



## Examples
<details open>
  <summary>TaskManager Examples</summary>

### Full TaskManager examples
 
<a id="tm-runasync-t"></a>
<details>
  <summary>[#TME1] Run an async task that returns a value</summary>
  - Run an async task that returns a value (await and get result). Use priority and cancellation:

  ```csharp
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  CancellationTokenSource cts = new CancellationTokenSource();
  int result = await NeraTools.TaskManager.TaskSchedulerEngine.RunAsync<int>(
      async token =>
      {
          // Simulate work
          await Task.Delay(1000, token);
          return 42;
      },
      ePriorityLevel.HighLevel,
      cts.Token);

  Console.WriteLine($"Result: {result}");
  ```
</details>

<a id="tm-runasync"></a>
<details>
  <summary>[#TME2] Run an async task without result</summary>
  - Run an async task without result (fire-and-forget) and await it if needed:

  ```csharp
  // Fire-and-forget
  _ = NeraTools.TaskManager.TaskSchedulerEngine.RunAsync(
      async ct =>
      {
          await Task.Delay(2000, ct);
          Console.WriteLine("Background work completed");
      },
      ePriorityLevel.MidLevel);

  // Or await when you need to wait
  await NeraTools.TaskManager.TaskSchedulerEngine.RunAsync(
      async ct => await Task.Delay(500, ct),
      ePriorityLevel.LowLevel);
  ```
</details>

<a id="tm-runsyncasasync-t"></a>
<details>
  <summary>[#TME3] Convert a synchronous function to async with result</summary>
  - Convert a CPU-bound synchronous function to an async task and await the result:

  ```csharp
  int sum = await NeraTools.TaskManager.TaskSchedulerEngine.RunSyncAsAsync<int>(
      () =>
      {
          int a = 10;
          int b = 20;
          // Heavy CPU work simulation
          for (int i = 0; i < 1_000_000; i++) { a += (i & 1); }
          return a + b;
      },
      ePriorityLevel.MidLevel);

  Console.WriteLine($"Sum: {sum}");
  ```
</details>

<a id="tm-runsyncasasync"></a>
<details>
  <summary>[#TME4] Run a synchronous action as background task</summary>
  - Run a synchronous action as a background task (fire-and-forget):

  ```csharp
  NeraTools.TaskManager.TaskSchedulerEngine.RunSyncAsAsync(
      () => Console.WriteLine("Background logging"),
      ePriorityLevel.LowLevel);
  ```
</details>

<a id="tm-setthreads-bypercent"></a>
<details>
  <summary>[#TME5] Set thread pool usage by CPU percentage</summary>
  - Set thread pool usage by CPU percentage (example uses predefined enum value):

  ```csharp
  // Example enum: eThreadUsagePercent.Perc50
  NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByPercen(eThreadUsagePercent.Perc50);
  ```
</details>

<a id="tm-setthreads-bycore"></a>
<details>
  <summary>[#TME6] Set threads count based on CPU cores</summary>
  - Set threads count based on CPU cores:

  ```csharp
  int logicalCores = Environment.ProcessorCount;
  NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByCore(logicalCores);
  ```
</details>

<a id="tm-setthreads-bythreads"></a>
<details>
  <summary>[#TME7] Set exact number of worker threads</summary>
  - Set exact number of worker threads:

  ```csharp
  NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByThreads(8);
  ```
</details>

<a id="tm-shutdown-immediate"></a>
<details>
  <summary>[#TME8] Immediate shutdown and cancel tasks</summary>
  - Immediate shutdown and cancel all running tasks:

  ```csharp
  NeraTools.TaskManager.TaskSchedulerEngine.ShutdownNeraToolImmediate();
  ```
</details>

<a id="tm-shutdown-graceful"></a>
<details>
  <summary>[#TME9] Graceful shutdown with timeout</summary>
  - Graceful shutdown with timeout in seconds (waits up to given seconds):

  ```csharp
  bool stopped = await NeraTools.TaskManager.TaskSchedulerEngine.ShutdownNeraToolGracefulAsync(5);
  Console.WriteLine($"Stopped gracefully: {stopped}");
  ```
</details>

<a id="tm-getrunningcount"></a>
<details>
  <summary>[#TME10] Get running task count</summary>
  - Get number of currently running tasks:

  ```csharp
  int running = NeraTools.TaskManager.TaskSchedulerEngine.GetRunningTaskCount();
  Console.WriteLine($"Running tasks: {running}");
  ```
</details>

<a id="tm-taskmonitor"></a>
<details>
  <summary>[#TME11] Start/stop the console task monitor</summary>
  - Start and stop the console task monitor. The boolean parameter indicates start(true)/stop(false).

  ```csharp
  using System.Threading;

  CancellationTokenSource monitorCts = new CancellationTokenSource();
  // Start monitor with 1000 ms refresh interval
  NeraTools.TaskManager.TaskSchedulerEngine.TaskMonitor(true, 1000, monitorCts.Token);

  // ... later stop
  monitorCts.Cancel();
  NeraTools.TaskManager.TaskSchedulerEngine.TaskMonitor(false, 0, CancellationToken.None);
  ```
</details>

<a id="tm-setdelay-ms"></a>
<details>
  <summary>[#TME12] Configure scheduler delays (ms)</summary>
  - Configure scheduler delays in milliseconds:

  ```csharp
  // activeMs, idleMs, maxIdleMs
  NeraTools.TaskManager.TaskSchedulerEngine.SetDelayTimeMilliseconds(50, 200, 2000);
  ```
</details>

<a id="tm-setdelay-sec"></a>
<details>
  <summary>[#TME13] Configure scheduler delays (sec)</summary>
  - Configure scheduler delays in seconds (wrapper over ms method):

  ```csharp
  // activeSec, idleSec, maxIdleSec
  NeraTools.TaskManager.TaskSchedulerEngine.SetDelayTimeSeconds(0, 1, 5);
  ```
</details>

---

<details open>
  <summary>ProgramOps Examples</summary>

### Full ProgramOps examples

<a id="poe-run"></a>
<details>
  <summary>[#POE1] Run programs (sync)</summary>
  - Run a single program synchronously and inspect the result. Also demonstrates running multiple paths at once and basic error handling:

  ```csharp
  using System;

  // Run single executable synchronously
  ProcessRunResult res = NeraTools.ProgramOps.Run("C:\\Tools\\myApp.exe");
  Console.WriteLine($"Success: {res.Success}, ExitCode: {res.ExitCode}");

  // Run multiple executables synchronously
  var list = new List<string> { "C:\\Tools\\a.exe", "C:\\Tools\\b.exe" };
  ProcessRunResult multi = NeraTools.ProgramOps.Run(list);
  Console.WriteLine($"Launched count: {multi?.LaunchedCount ?? 0}");
  ```
</details>

<a id="poe-run-async"></a>
<details>
  <summary>[#POE2] Run programs (awaitable async)</summary>
  - Awaitable run using the TaskSchedulerEngine. Demonstrates cancellation handling, timeout pattern and result inspection:

  ```csharp
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // auto-timeout
  try
  {
      ProcessRunResult result = await NeraTools.ProgramOps.RunAsync("C:\\Tools\\myApp.exe", cts.Token);
      Console.WriteLine($"ExitCode: {result.ExitCode}, Success: {result.Success}");
  }
  catch (OperationCanceledException)
  {
      Console.WriteLine("Run was canceled or timed out");
  }
  catch (Exception ex)
  {
      Console.WriteLine($"Run failed: {ex.Message}");
  }
  ```
</details>

<a id="poe-run-fire"></a>
<details>
  <summary>[#POE3] Run programs (fire-and-forget via scheduler)</summary>
  - Schedule a background run with priority (no await). Useful for non-blocking workflows and background tasks:

  ```csharp
  // Fire-and-forget scheduled run with mid priority
  NeraTools.ProgramOps.RunAsync("C:\\Tools\\myApp.exe", ePriorityLevel.MidLevel);

  // Keep in mind: use CancellationToken overload if you may need to cancel later.
  ```
</details>

<a id="poe-run-delay"></a>
<details>
  <summary>[#POE4] Run programs after a delay</summary>
  - Examples for delayed execution, both sync and awaitable async. Demonstrates common use: staged startup.

  ```csharp
  // Synchronous delayed run (start after 5 seconds)
  ProcessRunResult delayed = NeraTools.ProgramOps.Run("C:\\Tools\\myApp.exe", 5);
  Console.WriteLine($"Delayed launched: {delayed.Success}");

  // Async delayed run with cancellation support
  var delayedAsync = await NeraTools.ProgramOps.RunAsync("C:\\Tools\\myApp.exe", 5, CancellationToken.None);
  Console.WriteLine($"Delayed exit code: {delayedAsync.ExitCode}");
  ```
</details>

<a id="poe-run-arch"></a>
<details>
  <summary>[#POE5] Run architecture-specific executables</summary>
  - Provide separate binaries for x64 and x86; API picks appropriate one based on OS bitness. Shows both sync and async usage and fallback idea:

  ```csharp
  // Synchronous: provide x64 and x86 paths
  var archRes = NeraTools.ProgramOps.Run("C:\\Bin\\app-x64.exe", "C:\\Bin\\app-x86.exe");
  Console.WriteLine($"Picked variant launched: {archRes.Success}");

  // Async variant (with cancellation)
  var archAsync = await NeraTools.ProgramOps.RunAsync("C:\\Bin\\app-x64.exe", "C:\\Bin\\app-x86.exe", CancellationToken.None);
  Console.WriteLine($"Async picked exit code: {archAsync.ExitCode}");
  ```
</details>

<a id="poe-term"></a>
<details>
  <summary>[#POE6] Terminate processes (by path/name/PID) synchronously</summary>
  - Examples showing common termination patterns and examining results. Use `isJustNow` to force immediate kill when necessary:

  ```csharp
  // Terminate by process name
  var byName = NeraTools.ProgramOps.TerminateByName("notepad.exe");
  Console.WriteLine($"Terminated by name success: {byName.Success}");

  // Terminate by exact executable path
  var byPath = NeraTools.ProgramOps.TerminateByPath("C:\\Tools\\myApp.exe");
  Console.WriteLine($"Terminated by path: {byPath.Success}");

  // Terminate by PID (force immediate)
  var byPid = NeraTools.ProgramOps.TerminateByPID(12345, isJustNow: true);
  Console.WriteLine($"Terminated PID result: {byPid.Success}");
  ```
</details>

<a id="poe-term-async"></a>
<details>
  <summary>[#POE7] Terminate processes asynchronously (awaitable and scheduled)</summary>
  - Awaitable termination with cancellation and scheduled (fire-and-forget) termination with priority. Shows checking return details:

  ```csharp
  // Awaitable termination with cancellation
  CancellationTokenSource ctsTerm = new CancellationTokenSource();
  try
  {
      var asyncTerm = await NeraTools.ProgramOps.TerminateByNameAsync("notepad.exe", ctsTerm.Token);
      Console.WriteLine($"Async terminate success: {asyncTerm.Success}, Count: {asyncTerm.TerminatedCount}");
  }
  catch (OperationCanceledException) { Console.WriteLine("Termination canceled"); }

  // Schedule termination in background (no await) with priority
  NeraTools.ProgramOps.TerminateByPathAsync("C:\\Tools\\myApp.exe", ePriorityLevel.MidLevel);
  ```
</details>

<a id="poe-getpid"></a>
<details>
  <summary>[#POE8] Get running process IDs (sync & async)</summary>
  - Examples showing how to retrieve and iterate PIDs and use path-based filtering:

  ```csharp
  // Synchronous lookup by name
  List<int> pids = NeraTools.ProgramOps.GetPID("notepad.exe");
  foreach (var pid in pids) Console.WriteLine($"Found PID: {pid}");

  // Asynchronous lookup
  var apids = await NeraTools.ProgramOps.GetPIDAsync("notepad.exe", CancellationToken.None);
  Console.WriteLine($"Async found {apids.Count} instances");
  ```
</details>

<a id="poe-isexisted"></a>
<details>
  <summary>[#POE9] Check whether an app is running (sync & async)</summary>
  - Use boolean checks to verify running state; includes cancellation example for async check and a typical usage pattern:

  ```csharp
  // Synchronous check
  bool exists = NeraTools.ProgramOps.isExitedByName("notepad.exe");
  Console.WriteLine($"Notepad running: {exists}");

  // Asynchronous check with cancellation token and timeout
  CancellationTokenSource cts2 = new CancellationTokenSource(TimeSpan.FromSeconds(3));
  bool aexists = await NeraTools.ProgramOps.isExitedByNameAsync("notepad.exe", cts2.Token);
  Console.WriteLine($"Async notepad running: {aexists}");
  ```
</details>

---

<details open>
  <summary>Logging Examples</summary>

### Full Logging examples
<a id="lm-log"></a>
<details>
  <summary>[#LME1] Log a user application message</summary>
  - Basic usage: log a message with default type (Info):

  ```csharp
  NeraTools.LogManager.Logger.log("Application started", eLogType.Info);
  ```
</details>

<a id="lm-logForThisTool"></a>
<details>
  <summary>[#LME2] Log a framework/internal message</summary>
  - Use for internal/framework diagnostics:

  ```csharp
  NeraTools.LogManager.Logger.logForThisTool("Background service initialized", eLogType.Debug);
  ```
</details>

<a id="lm-writeConsole"></a>
<details>
  <summary>[#LME3] Enable/disable console logging</summary>

  ```csharp
  // Enable console log output
  NeraTools.LogManager.Logger.writeLogInConsole(true);

  // Disable console log output
  NeraTools.LogManager.Logger.writeLogInConsole(false);
  ```
</details>

<a id="lm-writeUi"></a>
<details>
  <summary>[#LME4] Enable/disable UI logging</summary>

  ```csharp
  NeraTools.LogManager.Logger.writeLogInUi(true);
  ```
</details>

<a id="lm-writeJson"></a>
<details>
  <summary>[#LME5] Write logs to JSON file</summary>
  - Enable JSON logging and set location/name:

  ```csharp
  NeraTools.LogManager.Logger.writeLogInJsonFile(true, "C:\\Logs", "AppLog");
  ```
</details>

<a id="lm-writeText"></a>
<details>
  <summary>[#LME6] Write logs to text file</summary>

  ```csharp
  NeraTools.LogManager.Logger.writeLogInTextFile(true, "C:\\Logs", "AppLogText");
  ```
</details>

<a id="lm-autoOffline"></a>
<details>
  <summary>[#LME7] Enable/disable automatic offline mode</summary>

  ```csharp
  // Disable automatic offline mode
  NeraTools.LogManager.Logger.AutoOfflinetion(false);
  ```
</details>

<a id="lm-disable"></a>
<details>
  <summary>[#LME8] Disable logger system immediately</summary>

  ```csharp
  NeraTools.LogManager.Logger.DisableLoggerSystem();
  ```
</details>

---
