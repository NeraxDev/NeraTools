namespace NeraXTools
{
    public static partial class FileOps
    {
        // =========================
        // File Methods
        // =========================
        /// <summary>Create a file from a full path.</summary>
        public static void CreateFileFullPath(string fullPath) => throw null;

        /// <summary>Create files from a list of full paths.</summary>
        public static void CreateFileFullPath(List<string> fullPaths) => throw null;

        /// <summary>Create a file from directory + file name.</summary>
        public static void CreateFile(string directory, string fileName) => throw null;

        /// <summary>Create files from lists of directories + file names.</summary>
        public static void CreateFile(List<string> directories, List<string> fileNames) => throw null;

        // =========================
        // Async File Methods
        // =========================
        /// <summary>Async create a file from full path.</summary>
        public static Task CreateFileFullPath_Async(string fullPath) => Task.Run(() => { });

        /// <summary>Async create files from a list of full paths.</summary>
        public static Task CreateFileFullPath_Async(List<string> fullPaths) => Task.Run(() => { });

        /// <summary>Async create a file from directory + file name.</summary>
        public static Task CreateFile_Async(string directory, string fileName) => Task.Run(() => { });

        /// <summary>Async create files from lists of directories + file names.</summary>
        public static Task CreateFile_Async(List<string> directories, List<string> fileNames) => Task.Run(() => { });
    }
}