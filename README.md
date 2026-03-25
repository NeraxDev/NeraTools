# NeraTools

This repository contains the `NeraTools` library and several helper applications targeting .NET 10.

Below is a concise, organized reference of the high-level wrapper APIs (wrappers that call into core implementations).
Each table lists the wrapper/group name, a short English description, a short Persian description, and the primary source file(s).

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

## ProgramOps (Process execution & management)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) |
|---|---|---|---|
| `Run`, `RunAsync` | Run executables (single/multiple), optional delay and architecture variants (x64/x86). | اجرای برنامه‌ها با تأخیر و نسخهٔ معماری. | `Program Operations/Execte Program - API.cs`, `Execte Program - Core.cs` |
| `TerminateByPath`, `TerminateByName`, `TerminateByPID` (sync & async) | Terminate processes by path, name or PID. | خاتمه فرایندها بر اساس مسیر، نام یا PID. | `Program Operations/Execte Program - API.cs` |
| `GetPID`, `GetPIDAsync`, `isExitedByName*` | Query running processes and check existence. | یافتن PIDها و بررسی وجود فرایندها. | `Program Operations/Execte Program - API.cs` |

---

## TaskSchedulerEngine / TaskManager (Task wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) | Example (see below) |
|---|---|---|---|---|
| `RunAsync<T>(Func<CancellationToken, Task<T>>)` | Runs an async task that returns a result; scheduled with priority. | اجرای تسک ناهمگام با مقدار خروجی و زمان‌بندی براساس اولویت. | `TaskManager/TaskManager - API.cs` | [Example](#tm-runasync-t) |
| `RunAsync(Func<CancellationToken, Task>)` | Runs an async task without result (fire-and-forget or awaited). | اجرای تسک ناهمگام بدون خروجی (Fire-and-forget یا با await). | `TaskManager/TaskManager - API.cs` | [Example](#tm-runasync) |
| `RunSyncAsAsync<T>(Func<T>)` | Converts a synchronous function into a scheduled async task returning a value. | تبدیل تابع همگام به تسک ناهمگام و بازگرداندن مقدار. | `TaskManager/TaskManager - API.cs` | [Example](#tm-runsyncasasync-t) |
| `RunSyncAsAsync(Action)` | Converts a synchronous Action into a scheduled async task (no result). | تبدیل اکشن همگام به تسک ناهمگام بدون خروجی. | `TaskManager/TaskManager - API.cs` | [Example](#tm-runsyncasasync) |
| `SetThreadsCountByPercen(eThreadUsagePercent)` | Configure thread pool usage based on CPU percentage for adaptive scaling. | تعیین میزان استفاده از رشته‌ها بر اساس درصد مصرف CPU. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bypercent) |
| `SetThreadsCountByCore(int)` | Set worker thread count based on CPU core count. | تنظیم تعداد رشته‌ها بر اساس تعداد هسته‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bycore) |
| `SetThreadsCountByThreads(int)` | Set exact number of worker threads. | تنظیم دقیق تعداد رشته‌های کاری. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setthreads-bythreads) |
| `ShutdownNeraToolImmediate()` | Immediately cancel all tasks and stop the engine. | خاموش‌سازی فوری موتور و کنسل‌کردن همه تسک‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-shutdown-immediate) |
| `ShutdownNeraToolGracefulAsync(int)` | Graceful shutdown: wait for running/queued tasks to finish (optional timeout). | خاموش‌سازی تدریجی با امکان تعیین زمان انتظار برای اتمام تسک‌ها. | `TaskManager/TaskManager - API.cs` | [Example](#tm-shutdown-graceful) |
| `GetRunningTaskCount()` | Returns number of currently running tasks. | تعداد تسک‌های درحال اجرا را برمی‌گرداند. | `TaskManager/TaskManager - API.cs` | [Example](#tm-getrunningcount) |
| `TaskMonitor(bool, int, CancellationToken)` | Start/stop console task monitor with refresh interval. | شروع/توقف نمایشگر کنسول تسک‌ها با نرخ به‌روزرسانی. | `TaskManager/TaskManager - API.cs` | [Example](#tm-taskmonitor) |
| `SetDelayTimeMilliseconds(int activeMs, int idleMs, int maxIdleMs)` | Configure scheduler delays in milliseconds for active/idle states. | تنظیم تاخیر زمان‌بندی‌کننده به میلی‌ثانیه برای حالات فعال و بیکاری. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setdelay-ms) |
| `SetDelayTimeSeconds(int activeSec, int idleSec, int maxIdleSec)` | Configure scheduler delays in seconds (wrapper over ms method). | تنظیم تاخیر زمان‌بندی‌کننده به ثانیه (شبه‌رپ) بر پایه متد میلی‌ثانیه. | `TaskManager/TaskManager - API.cs` | [Example](#tm-setdelay-sec) |

---



### Full TaskManager examples

<a id="tm-runasync-t"></a>
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

<a id="tm-runasync"></a>
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

<a id="tm-runsyncasasync-t"></a>
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

<a id="tm-runsyncasasync"></a>
- Run a synchronous action as a background task (fire-and-forget):

```csharp
NeraTools.TaskManager.TaskSchedulerEngine.RunSyncAsAsync(
    () => Console.WriteLine("Background logging"),
    ePriorityLevel.LowLevel);
```

<a id="tm-setthreads-bypercent"></a>
- Set thread pool usage by CPU percentage (example uses predefined enum value):

```csharp
// Example enum: eThreadUsagePercent.Perc50
NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByPercen(eThreadUsagePercent.Perc50);
```

<a id="tm-setthreads-bycore"></a>
- Set threads count based on CPU cores:

```csharp
int logicalCores = Environment.ProcessorCount;
NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByCore(logicalCores);
```

<a id="tm-setthreads-bythreads"></a>
- Set exact number of worker threads:

```csharp
NeraTools.TaskManager.TaskSchedulerEngine.SetThreadsCountByThreads(8);
```

<a id="tm-shutdown-immediate"></a>
- Immediate shutdown and cancel all running tasks:

```csharp
NeraTools.TaskManager.TaskSchedulerEngine.ShutdownNeraToolImmediate();
```

<a id="tm-shutdown-graceful"></a>
- Graceful shutdown with timeout in seconds (waits up to given seconds):

```csharp
bool stopped = await NeraTools.TaskManager.TaskSchedulerEngine.ShutdownNeraToolGracefulAsync(5);
Console.WriteLine($"Stopped gracefully: {stopped}");
```

<a id="tm-getrunningcount"></a>
- Get number of currently running tasks:

```csharp
int running = NeraTools.TaskManager.TaskSchedulerEngine.GetRunningTaskCount();
Console.WriteLine($"Running tasks: {running}");
```

<a id="tm-taskmonitor"></a>
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

<a id="tm-setdelay-ms"></a>
- Configure scheduler delays in milliseconds:

```csharp
// activeMs, idleMs, maxIdleMs
NeraTools.TaskManager.TaskSchedulerEngine.SetDelayTimeMilliseconds(50, 200, 2000);
```

<a id="tm-setdelay-sec"></a>
- Configure scheduler delays in seconds (wrapper over ms method):

```csharp
// activeSec, idleSec, maxIdleSec
NeraTools.TaskManager.TaskSchedulerEngine.SetDelayTimeSeconds(0, 1, 5);
```

---

## Logger / LogManager (Logging wrappers)

| Wrapper / Methods | English (short) | فارسی (کوتاه) | Source file(s) |
|---|---|---|---|
| `log`, `logForThisTool` | Log messages with categories and types. | ثبت پیام‌های لاگ با دسته‌بندی و نوع. | `LogManager/Logging - API.cs`, `Logging - Core.cs` |
| `writeLogInConsole`, `writeLogInUi`, `writeLogInJsonFile`, `writeLogInTextFile` | Configure logging targets (console, UI, JSON, text). | تنظیم مقصدهای خروجی لاگ (کنسول، UI، JSON، متنی). | `LogManager/Logging - API.cs`, `DTOs.cs` |
| `AutoOfflinetion`, `DisableLoggerSystem` | Toggle offline mode and disable logger. | فعال/غیرفعال کردن حالت آفلاین و غیرفعال‌سازی لاگر. | `LogManager/Logging - API.cs` |

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

