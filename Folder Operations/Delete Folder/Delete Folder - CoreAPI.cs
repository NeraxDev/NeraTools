using NeraTools.TaskManager;
using System.Runtime;

namespace NeraTools
{
    internal static partial class FolderOpsCore
    {
        private static void DeleteFolder_Sync(
                             string path,
                             //List<string> folderNames = null,
                             List<string> startWith = null,
                             List<string> contains_Name = null,
                             FileAttributes? attributes = null,
                             DateTime? creationStart = null,
                             DateTime? creationEnd = null,
                             DateTime? lastWriteStart = null,
                             DateTime? lastWriteEnd = null,
                             long? minSize = null,
                             long? maxSize = null,
                             bool filterEmptyFolders = false,
                             params FolderOps.FolderDeleteOptions[] options)
        {
            Func<DirectoryInfo, bool, Task> op = (dirInfo, recursive) =>
            {
                Directory.Delete(dirInfo.FullName, recursive);
                return Task.CompletedTask;
            };
            DeleteFolder_Core(
                                   path,
                                   /*folderNames,*/
                                   startWith,
                                   contains_Name,
                                   attributes,
                                   creationStart,
                                   creationEnd,
                                   lastWriteStart,
                                   lastWriteEnd,
                                   minSize,
                                   maxSize,
                                   filterEmptyFolders,
                                   op,
                                   options).GetAwaiter().GetResult();
        }

        private static async Task DeleteFolder_ParallelAsync(
                                  string path,
                                  int userInputThreads = 2,
                                  List<string> startWith = null,
                                  List<string> contains_Name = null,
                                  FileAttributes? attributes = null,
                                  DateTime? creationStart = null,
                                  DateTime? creationEnd = null,
                                  DateTime? lastWriteStart = null,
                                  DateTime? lastWriteEnd = null,
                                  long? minSize = null,
                                  long? maxSize = null,
                                  bool filterEmptyFolders = false,
                                  params FolderOps.FolderDeleteOptions[] options)
        {
            Func<DirectoryInfo, bool, Task> op = async (dirInfo, recursive) => await TaskSchedulerEngine.RunSyncAsAsync(() => Directory.Delete(dirInfo.FullName, recursive));

            await DeleteFolder_Core(
                                   path,
                                   startWith,
                                   contains_Name,
                                   attributes,
                                   creationStart,
                                   creationEnd,
                                   lastWriteStart,
                                   lastWriteEnd,
                                   minSize,
                                   maxSize,
                                   filterEmptyFolders,
                                   op,
                                   options);
        }
    } // end of Folder_Ops class
} // end of NeraTools namespace