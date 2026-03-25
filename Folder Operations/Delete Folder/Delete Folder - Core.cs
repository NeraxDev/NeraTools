using NeraTools.LogManager;

namespace NeraTools
{
    internal static partial class FolderOpsCore
    {
        private static async Task DeleteFolder_Core(
            string path,
            //List<string> folderNames = null,
            List<string> startWith = null,
            List<string> NameFilter = null,
            FileAttributes? attributes = null,
            DateTime? creationStart = null,
            DateTime? creationEnd = null,
            DateTime? lastWriteStart = null,
            DateTime? lastWriteEnd = null,
            long? minSize = null,
            long? maxSize = null,
            bool filterEmptyFolders = false,
            Func<DirectoryInfo, bool, Task> op = null,
            params FolderOps.FolderDeleteOptions[] options)
        {
            if (!Directory.Exists(path))
                return;

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            bool recursive = options.Contains(FolderOps.FolderDeleteOptions.recursive);
            bool retryIfInUse = options.Contains(FolderOps.FolderDeleteOptions.RetryIfInUse);
            bool logEnabled = options.Contains(FolderOps.FolderDeleteOptions.Logger);
            bool ignoreErrors = options.Contains(FolderOps.FolderDeleteOptions.IgnoreErrors);
            bool backupBeforeDelete = options.Contains(FolderOps.FolderDeleteOptions.BackupBeforeDeleate);

            try
            {
                // =========================
                // Filtering logic
                // =========================
                if (!await PassesFilters_Folder(dirInfo, NameFilter, minSize, maxSize, creationStart, creationEnd, lastWriteStart, lastWriteEnd, startWith, attributes))
                    return;

                if (filterEmptyFolders && dirInfo.GetDirectories().Length == 0 && dirInfo.GetFiles().Length == 0)
                {
                    Directory.Delete(path); // Delete empty folder immediately
                    return;
                }

                if (recursive)
                {
                    foreach (var subDir in dirInfo.GetDirectories())
                    {
                        await DeleteFolder_Core(subDir.FullName, /*folderNames,*/ startWith, NameFilter, attributes, creationStart, creationEnd, lastWriteStart, lastWriteEnd, minSize, maxSize, filterEmptyFolders, null, options);
                    }
                }

                bool deleted = false;
                int retryCount = 0;
                const int maxRetry = 3;
                while (!deleted && (!retryIfInUse || retryCount < maxRetry))
                {
                    try
                    {
                        if (backupBeforeDelete) //TODO: با متد بکاپ خودم باید جایگزین کنم
                        {
                            string backupPath = Path.Combine(Path.GetTempPath(), dirInfo.Name + "_Backup( " + DateTime.Now.Ticks + " )_");
                            await FolderOps.CopyFolder_Async(dirInfo.FullName, backupPath);
                            await op(dirInfo, recursive);
                            deleted = true;
                            if (logEnabled)
                                Logger.logForThisTool($"Folder backed up to {backupPath} \n Folder deleted: {{dirInfo.FullName}}");
                        }
                        else
                        {
                            await op(dirInfo, recursive);
                            deleted = true;
                            if (logEnabled)
                                Logger.logForThisTool($"Folder deleted: {dirInfo.FullName}");
                        }
                    }
                    catch (IOException)
                    {
                        if (!retryIfInUse)
                            throw;
                        retryCount++;
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                if (logEnabled)
                    Logger.logForThisTool($"Error : deleting folder {dirInfo.FullName}: {ex.Message}");

                if (!ignoreErrors)
                    throw;
            }
        }

        // =========================
        // PassesFiltersFolder - Core ( Used by Delete Folder )
        // =========================
        private static async Task<bool> PassesFilters_Folder(DirectoryInfo Folder, List<string> namesFilter, long? minSize, long? maxSize, DateTime? creationStart, DateTime? creationEnd, DateTime? lastWriteStart, DateTime? lastWriteEnd, List<string> startWith, FileAttributes? attributes)
        {
            if (namesFilter != null && namesFilter.Count > 0 && !namesFilter.Contains(Folder.Name))
                return false;

            if ((creationStart.HasValue && Folder.CreationTime < creationStart.Value) || (creationEnd.HasValue && Folder.CreationTime > creationEnd.Value))
                return false;
            if ((lastWriteStart.HasValue && Folder.LastWriteTime < lastWriteStart.Value) || (lastWriteEnd.HasValue && Folder.LastWriteTime > lastWriteEnd.Value))
                return false;
            if (attributes.HasValue && (Folder.Attributes & attributes.Value) != attributes.Value)
                return false;
            if (startWith != null && !startWith.Any(prefix => Folder.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
                return false;

            long folderSize = await GetFolderSizeParallel(Folder);

            if (minSize.HasValue && folderSize < minSize.Value)
                return false;
            if (maxSize.HasValue && folderSize > maxSize.Value)
                return false;

            return true;
        }

        private static async Task<long> GetFolderSizeParallel(DirectoryInfo dir)
        {
            long size = dir.GetFiles().Sum(f => f.Length);

            var tasks = dir.GetDirectories()
                           .Select(d => GetFolderSizeParallel(d))
                           .ToArray();

            var results = await Task.WhenAll(tasks);

            return size + results.Sum();
        }
    }
}