using NeraXTools.LogManager;
using NeraXTools.TaskManager;

namespace NeraXTools
{
    internal static partial class FolderOpsCore
    {
        //// =========================
        //// Create Folder Methods - Sync
        //// =========================
        internal static Result MakeFolder_Sync(List<string> folderPaths, List<string> folderNames = null, IProgress<float> p = null)
        {
            Func<string, Task<bool>> Op = (dir) =>
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    return Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    Logger.logForThisTool($"Can't make Folder \n Error: {ex}", eLogType.Error, eLogRecordMode.UI);
                    return Task.FromResult(false);
                }
            };
            return MakeFolder_Core(Op, folderPaths, folderNames, p).GetAwaiter().GetResult();
        }

        //// ===========================
        //// Create Folder Methods - Async
        //// ===========================
        internal static async Task<Result> MakeFolder_Async(List<string> folderPaths, List<string> folderNames = null, IProgress<float> p = null, ePriorityLevel? PL = ePriorityLevel.MidLevel, CancellationToken token = default)
        {
            Func<string, Task<bool>> Op = dir =>
                TaskSchedulerEngine.RunSyncAsAsync<bool>(() =>
                {
                    try
                    {
                        Directory.CreateDirectory(dir);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.logForThisTool($"Can't make Folder \n Error: {ex}", eLogType.Error, eLogRecordMode.UI);
                        return false;
                    }
                }, token);

            return await MakeFolder_Core(Op, folderPaths, folderNames, p);
        }
    } // end of Folder_Ops class
} // end of NeraXTools namespace