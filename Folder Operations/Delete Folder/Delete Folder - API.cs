namespace NeraXTools
{
    public static partial class FolderOps
    {
        public static void DeleteFolder(string path, FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders(List<string> paths, FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByName(string path, List<string> folderNames, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByName(List<string> paths, List<string> folderNames, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByStartWith(string path, List<string> startWith, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByStartWith(List<string> paths, List<string> startWith, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByContains(string path, List<string> contains, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByContains(List<string> paths, List<string> contains, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByAttributes(string path, FileAttributes attributes, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByAttributes(List<string> paths, FileAttributes attributes, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByCreationDate(string path, DateTime start, DateTime end, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByCreationDate(List<string> paths, DateTime start, DateTime end, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_ByLastWriteDate(string path, DateTime start, DateTime end, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolders_ByLastWriteDate(List<string> paths, DateTime start, DateTime end, params FolderDeleteOptions[] options) => throw null;

        public static void DeleteFolder_BySizeFilter(string path, long minSize, long maxSize = long.MaxValue, params FolderTransfomOptions[] options) => throw null;

        public static void DeleteFolders_BySizeFilter(List<string> paths, long minSize, long maxSize = long.MaxValue, params FolderTransfomOptions[] options) => throw null;

        public static void DeleteFolder_ByFilterEmptyFolders(string path, params FolderTransfomOptions[] options) => throw null;

        public static void DeleteFolders_ByFilterEmptyFolders(List<string> paths, params FolderTransfomOptions[] options) => throw null;

        public static void DeleteFolder(
                            string path,
                            List<string> folderNames = null,
                            List<string> startWith = null,
                            List<string> contains = null,
                            FileAttributes? attributes = null,
                            DateTime? creationStart = null,
                            DateTime? creationEnd = null,
                            DateTime? lastWriteStart = null,
                            DateTime? lastWriteEnd = null,
                            long? minSize = null,
                            long? maxSize = null,
                            bool filterEmptyFolders = false,
                            params FolderDeleteOptions[] options)
            => throw null;

        public static void DeleteFolders(
                            List<string> paths,
                            List<string> folderNames = null,
                            List<string> startWith = null,
                            List<string> contains = null,
                            FileAttributes? attributes = null,
                            DateTime? creationStart = null,
                            DateTime? creationEnd = null,
                            DateTime? lastWriteStart = null,
                            DateTime? lastWriteEnd = null,
                            long? minSize = null,
                            long? maxSize = null,
                            bool filterEmptyFolders = false,
                            params FolderDeleteOptions[] options)
        => throw null;
    } // end of Folder_Ops class
} // end of NeraXTools namespace