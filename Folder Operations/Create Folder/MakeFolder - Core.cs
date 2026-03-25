namespace NeraTools
{
    internal static partial class FolderOpsCore
    {
        private static async Task MakeFolder_Core(
                                  Func<string, Task> makeDir,
                                  List<string> folderPaths,
                                  List<string> folderNames = null)
        {
            List<string> pathsToCreate = new List<string>();

            try
            {
                // Case: only folder paths provided, no folder names
                if (folderNames == null)
                {
                    foreach (var path in folderPaths)
                        pathsToCreate.Add(path);
                }
                else
                {
                    // Case: equal count of paths and names
                    if (folderPaths.Count == folderNames.Count)
                    {
                        for (int i = 0; i < folderPaths.Count; i++)
                            AddFullPath(folderPaths[i], folderNames[i]);
                    }
                    // Case: single path, multiple folder names
                    else if (folderPaths.Count <= 1)
                    {
                        for (int i = 0; i < folderNames.Count; i++)
                            AddFullPath(folderPaths[0], folderNames[i]);
                    }
                    // Case: multiple paths, single folder name
                    else if (folderNames.Count <= 1)
                    {
                        for (int i = 0; i < folderPaths.Count; i++)
                            AddFullPath(folderPaths[i], folderNames[0]);
                    }
                    else
                    {
                        //Logger.log("Folder creation input count mismatch. Please provide correct counts.", false, Log_Type_Error);
                    }
                }
                foreach (var dir in pathsToCreate)
                    await makeDir(dir);
            }
            catch (Exception ex)
            {
                //Logger.log($"Unexpected error in MakeFolder_Async: {ex.Message}", false, Log_Type_Error);
            }

            // Local helper: combine path + folder name and add to list
            void AddFullPath(string basePath, string folderName)
            {
                try
                {
                    if (Directory.Exists(basePath))
                        pathsToCreate.Add(Path.Combine(basePath, folderName));
                    else
                    { /*Logger.log($"Base path does not exist: {basePath}", false, Log_Type_Warning);*/}
                }
                catch (Exception ex)
                {
                    //Logger.log($"Error combining path '{basePath}' with folder name '{folderName}': {ex.Message}", false, Log_Type_Error);
                }
            }
        }
    }
}