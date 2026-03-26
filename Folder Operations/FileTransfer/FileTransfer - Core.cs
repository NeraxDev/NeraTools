using NeraXTools.TaskManager;
using NeraXTools.LogManager;

namespace NeraXTools
{
    internal static partial class FolderOpsCore

    {
        // =========================
        // TransformOptionsFolder - Core ( Used by both MoveFolder and CopyFolder )
        // =========================

        private static async Task TransformOptionsFolder_Core(
            List<string> sourcePaths,
            List<string> destinationPaths,
            Transform_Options transform_Options,
            Func<string, string, bool, Transform_Options, Task> op,
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
            int totalPairs = Math.Min(sourcePaths.Count, destinationPaths.Count);

            try
            {
                for (int i = 0; i < totalPairs; i++)
                {
                    string sourcePath = sourcePaths[i];
                    string destinationPath = destinationPaths[i];

                    if (!Directory.Exists(sourcePath))
                    {
                        if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                            Logger.logForThisTool($"Source folder does not exist: {sourcePath}");
                        continue; // move to next pair
                    }

                    await MakeFolder_Async(new List<string> { destinationPath });

                    var allFiles = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories);

                    foreach (var filePath in allFiles)
                    {
                        var fileInfo = new FileInfo(filePath);

                        // =========================
                        // Apply filters
                        // =========================
                        if (!PassesFilters_File(fileInfo, namesFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, StartWith, attributes))
                            continue;
                        string relativePath = Path.GetRelativePath(sourcePath, filePath);
                        string destFile = Path.Combine(destinationPath, relativePath);

                        try
                        {
                            await MakeFolder_Async(new List<string> { Path.GetDirectoryName(destFile) });

                            bool fileExists = File.Exists(destFile);

                            if (options.Contains(FolderOps.FolderTransfomOptions.OverwriteIfNewer) && fileExists)
                            {
                                var destInfo = new FileInfo(destFile);
                                if (fileInfo.LastWriteTime <= destInfo.LastWriteTime)
                                {
                                    if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                                        Logger.logForThisTool($"Skipping file (destination newer): {destFile}");
                                    continue;
                                }
                            }

                            if (options.Contains(FolderOps.FolderTransfomOptions.SkipExisting) && fileExists)
                            {
                                if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                                    Logger.logForThisTool($"Skipping existing file: {destFile}");
                                continue;
                            }

                            if (options.Contains(FolderOps.FolderTransfomOptions.BackupBeforeOverwrite) && fileExists)
                            {
                                string backupPath = destFile + ".bak";
                                File.Move(destFile, backupPath, true);
                                if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                                    Logger.logForThisTool($"Backup created: {backupPath}");
                            }

                            if (options.Contains(FolderOps.FolderTransfomOptions.UseTempFiles))
                            {
                                string tempFile = destFile + ".tmp";
                                await op(filePath, tempFile, options.Contains(FolderOps.FolderTransfomOptions.Overwrite), Transform_Options.Copy);
                                await op(tempFile, destFile, options.Contains(FolderOps.FolderTransfomOptions.Overwrite), transform_Options);
                            }
                            else
                            {
                                await op(filePath, destFile, options.Contains(FolderOps.FolderTransfomOptions.Overwrite), transform_Options);
                            }

                            if (options.Contains(FolderOps.FolderTransfomOptions.PreserveAttributes))
                                File.SetAttributes(destFile, fileInfo.Attributes);

                            if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                                Logger.logForThisTool($"Moved: {filePath} -> {destFile}");
                        }
                        catch (Exception ex)
                        {
                            if (options.Contains(FolderOps.FolderTransfomOptions.Logger))
                                Logger.logForThisTool($"Error Moveing {filePath} -> {destFile}: {ex.Message}");

                            if (!options.Contains(FolderOps.FolderTransfomOptions.IgnoreErrors))
                                throw;
                        }
                    }
                }
            }
            catch (Exception ex) { Logger.logForThisTool($"MoveFolder fatal error: {ex.Message}"); }
        }

        // =========================
        // PassesFiltersFile - Core ( Used by both MoveFolder and CopyFolder )
        // =========================
        private static bool PassesFilters_File(FileInfo file, List<string> namesFilter, List<string> extensions, long minSize, long maxSize, DateTime? creationStart, DateTime? creationEnd, DateTime? lastWriteStart, DateTime? lastWriteEnd, List<string> startWith, FileAttributes? attributes)
        {
            if (file == null) return false;
            if (namesFilter != null && namesFilter.Count > 0 && !namesFilter.Contains(file.Name))
                return false;
            if (extensions != null && extensions.Count > 0 && !extensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase))
                return false;
            if (file.Length < minSize || file.Length > maxSize)
                return false;
            if ((creationStart.HasValue && file.CreationTime < creationStart.Value) || (creationEnd.HasValue && file.CreationTime > creationEnd.Value))
                return false;
            if ((lastWriteStart.HasValue && file.LastWriteTime < lastWriteStart.Value) || (lastWriteEnd.HasValue && file.LastWriteTime > lastWriteEnd.Value))
                return false;
            if (attributes.HasValue && (file.Attributes & attributes.Value) != attributes.Value)
                return false;
            if (startWith != null && !startWith.Any(prefix => file.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
                return false;

            return true;
        }

        private static readonly Func<string, string, bool, Transform_Options, Task> Op_Sync = (src, dest, overwrite, transform_Op) =>
        {
            if (transform_Op.Equals(Transform_Options.Move))
                File.Move(src, dest, overwrite);
            else if (transform_Op.Equals(Transform_Options.Copy))
                File.Copy(src, dest, overwrite);
            return Task.CompletedTask;
        };

        private static readonly Func<string, string, bool, Transform_Options, Task> Op_Async = (src, dest, overwrite, transform_Op) =>
        {
            if (transform_Op.Equals(Transform_Options.Move))
                TaskSchedulerEngine.RunSyncAsAsync(() => File.Move(src, dest, overwrite));
            else if (transform_Op.Equals(Transform_Options.Copy))
                TaskSchedulerEngine.RunSyncAsAsync(() => File.Copy(src, dest, overwrite));
            return Task.CompletedTask;
        };
    }
}