using NeraTools.TaskManager;

namespace NeraTools
{
    public static partial class FolderOps
    {
        // =========================
        // Create Folder Methods - Sync
        // =========================

        /// <summary>
        /// English : Creates a single folder using the full path provided.
        /// Farsi  : ساخت یک فولدر با مسیر کامل
        /// </summary>
        /// <param name="fullPath">The complete path including the folder name to create. / مسیر کامل شامل نام فولدر</param>
        /// <remarks>
        /// Checks if the folder exists before creating it. If it exists, does nothing.
        /// استفاده وقتی که مسیر کامل فولدر آماده است.
        /// </remarks>
        public static void CreateFolderFullPath(string fullPath) => FolderOpsCore.MakeFolder_Sync(new List<string> { fullPath });

        /// <summary>
        /// English : Creates multiple folders using a list of full paths.
        /// Farsi  : ساخت چند فولدر با لیست مسیرهای کامل
        /// </summary>
        /// <param name="fullPaths">A list of full folder paths. / لیست مسیرهای کامل فولدرها</param>
        /// <remarks>
        /// Iterates through the list and creates each folder if it does not exist.
        /// مناسب زمانی که می‌خواهید چند فولدر را یک‌جا بسازید.
        /// </remarks>
        public static void CreateFolderFullPath(List<string> fullPaths) => FolderOpsCore.MakeFolder_Sync(fullPaths);

        /// <summary>
        /// English : Creates multiple folders using a variable number of full path strings.
        /// Farsi  : ساخت چند فولدر با چند مسیر کامل متغیر
        /// </summary>
        /// <param name="fullPaths">An array of full folder paths. / آرایه‌ای از مسیرهای کامل فولدرها</param>
        /// <remarks>
        /// Similar to the List&lt;string&gt; version, allows inline multiple paths.
        /// مثال: CreateFolderFullPath("C:\\A", "C:\\B", "C:\\C")
        /// </remarks>
        public static void CreateFolderFullPath(params string[] fullPaths) => FolderOpsCore.MakeFolder_Sync(fullPaths.ToList());

        /// <summary>
        /// English : Creates a folder inside the specified directory with a given folder name.
        /// Farsi  : ساخت فولدر داخل یک مسیر مشخص با نام دلخواه
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <remarks>
        /// Checks if the parent directory exists and creates the folder accordingly.
        /// مناسب وقتی مسیر والد و نام فولدر جدا هست.
        /// </remarks>
        public static void CreateFolder(string directory, string folderName) => FolderOpsCore.MakeFolder_Sync(new List<string> { directory }, new List<string> { folderName });

        /// <summary>
        /// English : Creates multiple folders in multiple directories using lists of directories and folder names.
        /// Farsi  : ساخت چند فولدر در چند مسیر مختلف با لیست مسیرها و نام‌ها
        /// </summary>
        /// <param name="directories">List of parent directories. / لیست مسیرهای والد</param>
        /// <param name="folderNames">List of folder names. / لیست نام فولدرها</param>
        /// <remarks>
        /// The lists must be the same length. Each folder in folderNames is created inside the corresponding directory.
        /// طول دو لیست باید برابر باشد.
        /// </remarks>
        public static void CreateFolder(List<string> directories, List<string> folderNames) => FolderOpsCore.MakeFolder_Sync(directories, folderNames);

        /// <summary>
        /// English : Creates multiple folders inside a single directory using a variable number of folder names.
        /// Farsi  : ساخت چند فولدر داخل یک مسیر مشخص با چند نام فولدر متغیر
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderNames">An array of folder names. / آرایه‌ای از نام فولدرها</param>
        /// <remarks>
        /// Example: CreateFolder("C:\\Temp", "A", "B", "C").
        /// مناسب زمانی که می‌خواهید چند فولدر داخل یک مسیر بسازید.
        /// </remarks>
        public static void CreateFolder(string directory, params string[] folderNames) => FolderOpsCore.MakeFolder_Sync(new List<string> { directory }, folderNames.ToList());

        /// <summary>
        /// English : Creates the same folder inside multiple parent directories.
        /// Farsi  : ساخت یک فولدر مشابه در چند مسیر والد
        /// </summary>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <param name="directories">An array of parent directories. / آرایه‌ای از مسیرهای والد</param>
        /// <remarks>
        /// Example: CreateFolder_InMultiDirectories("Logs", "C:\\App1", "D:\\App2")
        /// Creates "Logs" folder in all specified directories, only if it does not already exist.
        /// مناسب زمانی که می‌خواهید یک فولدر مشابه در چند مسیر بسازید.
        /// </remarks>
        public static void CreateFolder_InMultiDirectories(string folderName, params string[] directories) => FolderOpsCore.MakeFolder_Sync(directories.ToList(), new List<string> { folderName });

        // =========================
        // Create Folder Methods - Async
        // =========================

        /// <summary>
        /// English : Creates a single folder using the full path provided asynchronously.
        /// Farsi  : ساخت یک فولدر با مسیر کامل به صورت ای‌سینک
        /// </summary>
        /// <param name="fullPath">The complete path including the folder name to create. / مسیر کامل شامل نام فولدر</param>
        /// <remarks>
        /// Checks if the folder exists before creating it. If it exists, does nothing.
        /// استفاده وقتی که مسیر کامل فولدر آماده است.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderFullPathAsync(string fullPath, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(new List<string> { fullPath }, null, PL, token);

        /// <summary>
        /// English : Creates multiple folders using a list of full paths asynchronously.
        /// Farsi  : ساخت چند فولدر با لیست مسیرهای کامل به صورت ای‌سینک
        /// </summary>
        /// <param name="fullPaths">A list of full folder paths. / لیست مسیرهای کامل فولدرها</param>
        /// <remarks>
        /// Iterates through the list and creates each folder if it does not exist.
        /// مناسب زمانی که می‌خواهید چند فولدر را یک‌جا بسازید.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderFullPathAsync(List<string> fullPaths, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(fullPaths, null, PL, token);

        /// <summary>
        /// English : Creates multiple folders using a variable number of full path strings asynchronously.
        /// Farsi  : ساخت چند فولدر با چند مسیر کامل متغیر به صورت ای‌سینک
        /// </summary>
        /// <param name="fullPaths">An array of full folder paths. / آرایه‌ای از مسیرهای کامل فولدرها</param>
        /// <remarks>
        /// Similar to the List&lt;string&gt; version, allows inline multiple paths.
        /// مثال: CreateFolderFullPathAsync("C:\\A", "C:\\B", "C:\\C")
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderFullPathAsync(ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] fullPaths) => FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, PL, token);

        /// <summary>
        /// English : Creates a folder inside the specified directory with a given folder name asynchronously.
        /// Farsi  : ساخت فولدر داخل یک مسیر مشخص با نام دلخواه به صورت ای‌سینک
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <remarks>
        /// Checks if the parent directory exists and creates the folder accordingly.
        /// مناسب وقتی مسیر والد و نام فولدر جدا هست.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderAsync(string directory, string folderName, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(new List<string> { directory }, new List<string> { folderName }, PL, token);

        /// <summary>
        /// English : Creates multiple folders in multiple directories using lists of directories and folder names asynchronously.
        /// Farsi  : ساخت چند فولدر در چند مسیر مختلف با لیست مسیرها و نام‌ها به صورت ای‌سینک
        /// </summary>
        /// <param name="directories">List of parent directories. / لیست مسیرهای والد</param>
        /// <param name="folderNames">List of folder names. / لیست نام فولدرها</param>
        /// <remarks>
        /// The lists must be the same length. Each folder in folderNames is created inside the corresponding directory.
        /// طول دو لیست باید برابر باشد.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderAsync(List<string> directories, List<string> folderNames, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(directories, folderNames, PL, token);

        /// <summary>
        /// English : Creates multiple folders inside a single directory using a variable number of folder names asynchronously.
        /// Farsi  : ساخت چند فولدر داخل یک مسیر مشخص با چند نام فولدر متغیر به صورت ای‌سینک
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderNames">An array of folder names. / آرایه‌ای از نام فولدرها</param>
        /// <remarks>
        /// Example: CreateFolderAsync("C:\\Temp", "A", "B", "C").
        /// مناسب زمانی که می‌خواهید چند فولدر داخل یک مسیر بسازید.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderAsync(string directory, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] folderNames) => FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), PL, token);

        /// <summary>
        /// English : Creates the same folder inside multiple parent directories asynchronously.
        /// Farsi  : ساخت یک فولدر مشابه در چند مسیر والد به صورت ای‌سینک
        /// </summary>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <param name="directories">An array of parent directories. / آرایه‌ای از مسیرهای والد</param>
        /// <remarks>
        /// Example: CreateFolderInMultiDirectoriesAsync("Logs", "C:\\App1", "D:\\App2")
        /// Creates "Logs" folder in all specified directories, only if it does not already exist.
        /// مناسب زمانی که می‌خواهید یک فولدر مشابه در چند مسیر بسازید.
        /// </remarks>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
        public static Task CreateFolderInMultiDirectoriesAsync(string folderName, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] directories) => FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, PL, token);
    } // end of Folder_Ops class
} // end of NeraTools namespace