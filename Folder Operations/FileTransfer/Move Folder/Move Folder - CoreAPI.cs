using NeraTools.TaskManager;
using System.Runtime.CompilerServices;

namespace NeraTools
{
    internal static partial class FolderOpsCore
    {
        internal static void Move_Sync(
                    List<string> sourcePaths,
                    List<string> destinationPaths,
                    Transform_Options transform_Options,
                    List<string> namesFilter = null,
                    List<string> extensions = null,
                    long minSize = 0,
                    long maxSize = long.MaxValue,
                    DateTime? Creation_startDate = null,
                    DateTime? Creation_endDate = null,
                    DateTime? LastWrite_startDate = null,
                    DateTime? LastWrite_endDate = null,
                    List<string>? StartWith = null,
                    FileAttributes? attributes = null,
                    params FolderOps.FolderTransfomOptions[] options)
        {
            TransformOptionsFolder_Core(
                sourcePaths,
                destinationPaths,
                transform_Options,
                Op_Sync,
                namesFilter,
                extensions,
                minSize,
                maxSize,
                Creation_startDate,
                Creation_endDate,
                LastWrite_startDate,
                LastWrite_endDate,
                StartWith,
                attributes,
                options).GetAwaiter().GetResult();
        }

        internal static async Task Move_Async(
                            List<string> sourcePaths,
                            List<string> destinationPaths,
                            Transform_Options transform_Options,
                            List<string> namesFilter = null,
                            List<string> extensions = null,
                            long minSize = 0,
                            long maxSize = long.MaxValue,
                            DateTime? Creation_startDate = null,
                            DateTime? Creation_endDate = null,
                            DateTime? LastWrite_startDate = null,
                            DateTime? LastWrite_endDate = null,
                            List<string>? StartWith = null,
                            FileAttributes? attributes = null,
                            params FolderOps.FolderTransfomOptions[] options)
        {
            _ = TransformOptionsFolder_Core(
                  sourcePaths,
                  destinationPaths,
                  transform_Options,
                  Op_Async,
                  namesFilter,
                  extensions,
                  minSize,
                  maxSize,
                  Creation_startDate,
                  Creation_endDate,
                  LastWrite_startDate,
                  LastWrite_endDate,
                  StartWith,
                  attributes,
                  options);
        }
    } // end of Folder_Ops class
} // end of NeraTools namespace