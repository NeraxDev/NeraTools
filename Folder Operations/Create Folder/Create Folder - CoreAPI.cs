using NeraXTools.TaskManager;

namespace NeraXTools
{
    internal static partial class FolderOpsCore
    {
        //// =========================
        //// Create Folder Methods - Sync
        //// =========================
        internal static void MakeFolder_Sync(List<string> folderPaths, List<string> folderNames = null)
        {
            Func<string, Task> Op = (dir) => { Directory.CreateDirectory(dir); return Task.CompletedTask; };
            MakeFolder_Core(Op, folderPaths, folderNames).GetAwaiter().GetResult();
        }

        //// ===========================
        //// Create Folder Methods - Async
        //// ===========================
        internal static async Task MakeFolder_Async(List<string> folderPaths, List<string> folderNames = null, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
        {
            Func<string, Task> fileOp = dir => TaskSchedulerEngine.RunSyncAsAsync(() => { Directory.CreateDirectory(dir); }, PL, token);
            await MakeFolder_Core(fileOp, folderPaths, folderNames);
        }
    } // end of Folder_Ops class
} // end of NeraXTools namespace