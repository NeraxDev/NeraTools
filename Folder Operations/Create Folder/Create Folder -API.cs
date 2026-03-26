using NeraXTools;
using NeraXTools.TaskManager;

namespace NeraXTools
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
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Checks if the folder exists before creating it. If it exists, does nothing.
        /// استفاده وقتی که مسیر کامل فولدر آماده است.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(string fullPath, IProgress<float> progress = null) => FolderOpsCore.MakeFolder_Sync(new List<string> { fullPath }, null, progress);

        /// <summary>
        /// English : Creates multiple folders using a list of full paths.
        /// Farsi  : ساخت چند فولدر با لیست مسیرهای کامل
        /// </summary>
        /// <param name="fullPaths">A list of full folder paths. / لیست مسیرهای کامل فولدرها</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Iterates through the list and creates each folder if it does not exist.
        /// مناسب زمانی که می‌خواهید چند فولدر را یک‌جا بسازید.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(List<string> fullPaths, IProgress<float> progress = null) => FolderOpsCore.MakeFolder_Sync(fullPaths, null, progress);

        /// <summary>
        /// English : Creates multiple folders using a variable number of full path strings.
        /// Farsi  : ساخت چند فولدر با چند مسیر کامل متغیر
        /// </summary>
        /// <param name="fullPaths">An array of full folder paths. / آرایه‌ای از مسیرهای کامل فولدرها</param>
        /// <remarks>
        /// Similar to the List&lt;string&gt; version, allows inline multiple paths.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(params string[] fullPaths) =>
            FolderOpsCore.MakeFolder_Sync(fullPaths.ToList(), null, null);

        /// <summary>
        /// English : Creates multiple folders using a variable number of full path strings.
        /// Farsi  : ساخت چند فولدر با چند مسیر کامل متغیر
        /// </summary>
        /// <param name="fullPaths">An array of full folder paths. / آرایه‌ای از مسیرهای کامل فولدرها</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Similar to the List&lt;string&gt; version, allows inline multiple paths.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(IProgress<float> progress, params string[] fullPaths) =>
            FolderOpsCore.MakeFolder_Sync(fullPaths.ToList(), null, progress);

        /// <summary>
        /// English : Creates a folder inside the specified directory with a given folder name.
        /// Farsi  : ساخت فولدر داخل یک مسیر مشخص با نام دلخواه
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Checks if the parent directory exists and creates the folder accordingly.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(string directory, string folderName, IProgress<float> progress = null) => FolderOpsCore.MakeFolder_Sync(new List<string> { directory }, new List<string> { folderName }, progress);

        /// <summary>
        /// English : Creates multiple folders in multiple directories using lists of directories and folder names.
        /// Farsi  : ساخت چند فولدر در چند مسیر مختلف با لیست مسیرها و نام‌ها
        /// </summary>
        /// <param name="directories">List of parent directories. / لیست مسیرهای والد</param>
        /// <param name="folderNames">List of folder names. / لیست نام فولدرها</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// The lists must be the same length.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(List<string> directories, List<string> folderNames, IProgress<float> progress = null) => FolderOpsCore.MakeFolder_Sync(directories, folderNames, progress);

        /// <summary>
        /// English : Creates multiple folders inside a single directory using a variable number of folder names.
        /// Farsi  : ساخت چند فولدر داخل یک مسیر مشخص با چند نام فولدر متغیر
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderNames">An array of folder names. / آرایه‌ای از نام فولدرها</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Same as void version but returns result.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(string directory, IProgress<float> progress = null, params string[] folderNames) => FolderOpsCore.MakeFolder_Sync(new List<string> { directory }, folderNames.ToList(), progress);

        /// <summary>
        /// English : Creates multiple folders inside a single directory using a variable number of folder names.
        /// Farsi  : ساخت چند فولدر داخل یک مسیر مشخص با چند نام فولدر متغیر
        /// </summary>
        /// <param name="directory">The parent directory. / مسیر والد</param>
        /// <param name="folderNames">An array of folder names. / آرایه‌ای از نام فولدرها</param>
        /// <remarks>
        /// Same as void version but returns result.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolder(string directory, params string[] folderNames) => FolderOpsCore.MakeFolder_Sync(new List<string> { directory }, folderNames.ToList(), null);

        /// <summary>
        /// English : Creates the same folder inside multiple parent directories.
        /// Farsi  : ساخت یک فولدر مشابه در چند مسیر والد
        /// </summary>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <param name="directories">An array of parent directories. / آرایه‌ای از مسیرهای والد</param>
        /// <remarks>
        /// Same as void version but returns result.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolderInMultiDirectories(string folderName, params string[] directories) => FolderOpsCore.MakeFolder_Sync(directories.ToList(), new List<string> { folderName }, null);

        /// <summary>
        /// English : Creates the same folder inside multiple parent directories.
        /// Farsi  : ساخت یک فولدر مشابه در چند مسیر والد
        /// </summary>
        /// <param name="folderName">The folder name to create. / نام فولدر</param>
        /// <param name="directories">An array of parent directories. / آرایه‌ای از مسیرهای والد</param>
        /// <param name="progress">Reports operation progress (0-100). / گزارش میزان پیشرفت</param>
        /// <remarks>
        /// Same as void version but returns result.
        /// </remarks>
        /// <returns>
        /// Operation result including success/failure info.
        /// نتیجه عملیات شامل موفقیت یا خطا
        /// </returns>
        public static Result CreateFolderInMultiDirectories(string folderName, IProgress<float> progress = null, params string[] directories) => FolderOpsCore.MakeFolder_Sync(directories.ToList(), new List<string> { folderName }, progress);

        // =========================
        // Create Folder Methods - Async
        // =========================

        /// <summary>
        /// English : Creates a single folder asynchronously (Returns Result).
        /// Farsi  : ساخت یک فولدر به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(@"C:\NewFolder", progress, ePriorityLevel.High);
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(string fullPath, IProgress<float> progress = null, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => await FolderOpsCore.MakeFolder_Async(new List<string> { fullPath }, null, progress, PL, token);

        /// <summary>
        /// English : Creates a single folder asynchronously (Fire-and-forget).
        /// Farsi  : ساخت یک فولدر به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(@"C:\NewFolder", progress);
        /// </remarks>
        public static async Task CreateFolderAsync(string fullPath, IProgress<float> progress = null, CancellationToken token = default)
            => _ = FolderOpsCore.MakeFolder_Async(new List<string> { fullPath }, null, progress, null, token);

        /// <summary>
        /// English : Creates multiple folders from a list asynchronously (Returns Result).
        /// Farsi  : ساخت چند فولدر از لیست مسیرها به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(listOfPaths, progress, ePriorityLevel.Low);
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(List<string> fullPaths, IProgress<float> progress = null, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => await FolderOpsCore.MakeFolder_Async(fullPaths, null, progress, PL, token);

        /// <summary>
        /// English : Creates multiple folders from a list asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند فولدر از لیست مسیرها به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(listOfPaths, progress);
        /// </remarks>
        public static async Task CreateFolderAsync(List<string> fullPaths, IProgress<float> progress = null, CancellationToken token = default)
            => _ = FolderOpsCore.MakeFolder_Async(fullPaths, null, progress, null, token);

        /// <summary>
        /// English : Creates multiple folders using params asynchronously (Returns Result).
        /// Farsi  : ساخت چند فولدر با پارامترهای متغیر به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(ePriorityLevel.MidLevel, cts.Token, @"C:\F1", @"C:\F2");
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(ePriorityLevel PL, CancellationToken token, params string[] fullPaths)
            => await FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, null, PL, token);

        /// <summary>
        /// English : Creates multiple folders using params asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند فولدر با پارامترهای متغیر به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(cts.Token, @"C:\F1", @"C:\F2");
        /// </remarks>
        public static async Task CreateFolderAsync(CancellationToken token, params string[] fullPaths)
            => _ = FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, null, null, token);

        /// <summary>
        /// English : Creates multiple folders with progress using params asynchronously (Returns Result).
        /// Farsi  : ساخت چند فولدر همراه با گزارش پیشرفت به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(prog, ePriorityLevel.High, cts.Token, @"path1", @"path2");
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(IProgress<float> progress, ePriorityLevel PL, CancellationToken token, params string[] fullPaths)
            => await FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, progress, PL, token);

        /// <summary>
        /// English : Creates multiple folders with progress using params asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند فولدر همراه با گزارش پیشرفت به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(prog, cts.Token, @"path1", @"path2");
        /// </remarks>
        public static async Task CreateFolderAsync(IProgress<float> progress, CancellationToken token, params string[] fullPaths)
            => _ = FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, progress, null, token);

        /// <summary>
        /// English : Creates a folder in a specific directory asynchronously (Returns Result).
        /// Farsi  : ساخت فولدر در مسیر مشخص با نام دلخواه به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(@"C:\Base", "SubFolder", progress);
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(string directory, string folderName, IProgress<float> progress = null, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => await FolderOpsCore.MakeFolder_Async(new List<string> { directory }, new List<string> { folderName }, progress, PL, token);

        /// <summary>
        /// English : Creates a folder in a specific directory asynchronously (Fire-and-forget).
        /// Farsi  : ساخت فولدر در مسیر مشخص با نام دلخواه به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(@"C:\Base", "SubFolder", progress);
        /// </remarks>
        public static async Task CreateFolderAsync(string directory, string folderName, IProgress<float> progress = null, CancellationToken token = default)
            => _ = FolderOpsCore.MakeFolder_Async(new List<string> { directory }, new List<string> { folderName }, progress, null, token);

        /// <summary>
        /// English : Creates multiple folders from two lists asynchronously (Returns Result).
        /// Farsi  : ساخت چند فولدر با لیست مسیرها و نام‌ها به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(listDirs, listNames, progress);
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(List<string> directories, List<string> folderNames, IProgress<float> progress = null, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default)
            => await FolderOpsCore.MakeFolder_Async(directories, folderNames, progress, PL, token);

        /// <summary>
        /// English : Creates multiple folders from two lists asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند فولدر با لیست مسیرها و نام‌ها به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(listDirs, listNames, progress);
        /// </remarks>
        public static async Task CreateFolderAsync(List<string> directories, List<string> folderNames, IProgress<float> progress = null, CancellationToken token = default)
            => _ = FolderOpsCore.MakeFolder_Async(directories, folderNames, progress, null, token);

        /// <summary>
        /// English : Creates multiple subfolders in one directory with progress asynchronously (Returns Result).
        /// Farsi  : ساخت چند زیرفولدر در یک مسیر با گزارش پیشرفت به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(@"C:\Main", prog, ePriorityLevel.Mid, token, "Sub1", "Sub2");
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(string directory, IProgress<float> progress, ePriorityLevel PL, CancellationToken token, params string[] folderNames)
            => await FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), progress, PL, token);

        /// <summary>
        /// English : Creates multiple subfolders in one directory with progress asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند زیرفولدر در یک مسیر با گزارش پیشرفت به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(@"C:\Main", prog, token, "Sub1", "Sub2");
        /// </remarks>
        public static async Task CreateFolderAsync(string directory, IProgress<float> progress, CancellationToken token, params string[] folderNames)
            => _ = FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), progress, null, token);

        /// <summary>
        /// English : Creates multiple subfolders in one directory asynchronously (Returns Result).
        /// Farsi  : ساخت چند زیرفولدر در یک مسیر به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderAsync(@"C:\Main", ePriorityLevel.Low, token, "SubA", "SubB");
        /// </remarks>
        public static async Task<Result> CreateFolderAsync(string directory, ePriorityLevel PL, CancellationToken token, params string[] folderNames)
            => await FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), null, PL, token);

        /// <summary>
        /// English : Creates multiple subfolders in one directory asynchronously (Fire-and-forget).
        /// Farsi  : ساخت چند زیرفولدر در یک مسیر به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderAsync(@"C:\Main", token, "SubA", "SubB");
        /// </remarks>
        public static async Task CreateFolderAsync(string directory, CancellationToken token, params string[] folderNames)
            => _ = FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), null, null, token);

        /// <summary>
        /// English : Creates the same folder in multiple directories asynchronously (Returns Result).
        /// Farsi  : ساخت یک فولدر مشابه در چندین مسیر والد به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderInMultiDirectoriesAsync("Logs", ePriorityLevel.Mid, token, @"D:\App1", @"E:\App2");
        /// </remarks>
        public static async Task<Result> CreateFolderInMultiDirectoriesAsync(string folderName, ePriorityLevel PL, CancellationToken token, params string[] directories)
            => await FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, null, PL, token);

        /// <summary>
        /// English : Creates the same folder in multiple directories asynchronously (Fire-and-forget).
        /// Farsi  : ساخت یک فولدر مشابه در چندین مسیر والد به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderInMultiDirectoriesAsync("Logs", token, @"D:\App1", @"E:\App2");
        /// </remarks>
        public static async Task CreateFolderInMultiDirectoriesAsync(string folderName, CancellationToken token, params string[] directories)
            => _ = FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, null, null, token);

        /// <summary>
        /// English : Creates a folder in multiple directories with progress asynchronously (Returns Result).
        /// Farsi  : ساخت یک فولدر در چند مسیر با گزارش پیشرفت به صورت غیرهمزمان (دارای خروجی نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: await CreateFolderInMultiDirectoriesAsync("Data", prog, ePriorityLevel.High, token, @"C:\P1", @"D:\P2");
        /// </remarks>
        public static async Task<Result> CreateFolderInMultiDirectoriesAsync(string folderName, IProgress<float> progress, ePriorityLevel PL, CancellationToken token, params string[] directories)
            => await FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, progress, PL, token);

        /// <summary>
        /// English : Creates a folder in multiple directories with progress asynchronously (Fire-and-forget).
        /// Farsi  : ساخت یک فولدر در چند مسیر با گزارش پیشرفت به صورت غیرهمزمان (بدون انتظار برای نتیجه)
        /// </summary>
        /// <remarks>
        /// Example: CreateFolderInMultiDirectoriesAsync("Data", prog, token, @"C:\P1", @"D:\P2");
        /// </remarks>
        public static async Task CreateFolderInMultiDirectoriesAsync(string folderName, IProgress<float> progress, CancellationToken token, params string[] directories)
            => _ = FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, progress, null, token);
    } // end of Folder_Ops class
} // end of NeraXTools namespace

///// <summary>
///// English : Creates a single folder using the full path provided asynchronously.
///// Farsi  : ساخت یک فولدر با مسیر کامل به صورت ای‌سینک
///// </summary>
///// <param name="fullPath">The complete path including the folder name to create. / مسیر کامل شامل نام فولدر</param>
///// <remarks>
///// Checks if the folder exists before creating it. If it exists, does nothing.
///// استفاده وقتی که مسیر کامل فولدر آماده است.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderFullPathAsync(string fullPath, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(new List<string> { fullPath }, null, PL, token);

///// <summary>
///// English : Creates multiple folders using a list of full paths asynchronously.
///// Farsi  : ساخت چند فولدر با لیست مسیرهای کامل به صورت ای‌سینک
///// </summary>
///// <param name="fullPaths">A list of full folder paths. / لیست مسیرهای کامل فولدرها</param>
///// <remarks>
///// Iterates through the list and creates each folder if it does not exist.
///// مناسب زمانی که می‌خواهید چند فولدر را یک‌جا بسازید.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderFullPathAsync(List<string> fullPaths, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(fullPaths, null, PL, token);

///// <summary>
///// English : Creates multiple folders using a variable number of full path strings asynchronously.
///// Farsi  : ساخت چند فولدر با چند مسیر کامل متغیر به صورت ای‌سینک
///// </summary>
///// <param name="fullPaths">An array of full folder paths. / آرایه‌ای از مسیرهای کامل فولدرها</param>
///// <remarks>
///// Similar to the List&lt;string&gt; version, allows inline multiple paths.
///// مثال: CreateFolderFullPathAsync("C:\\A", "C:\\B", "C:\\C")
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderFullPathAsync(ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] fullPaths) => FolderOpsCore.MakeFolder_Async(fullPaths.ToList(), null, PL, token);

///// <summary>
///// English : Creates a folder inside the specified directory with a given folder name asynchronously.
///// Farsi  : ساخت فولدر داخل یک مسیر مشخص با نام دلخواه به صورت ای‌سینک
///// </summary>
///// <param name="directory">The parent directory. / مسیر والد</param>
///// <param name="folderName">The folder name to create. / نام فولدر</param>
///// <remarks>
///// Checks if the parent directory exists and creates the folder accordingly.
///// مناسب وقتی مسیر والد و نام فولدر جدا هست.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderAsync(string directory, string folderName, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(new List<string> { directory }, new List<string> { folderName }, PL, token);

///// <summary>
///// English : Creates multiple folders in multiple directories using lists of directories and folder names asynchronously.
///// Farsi  : ساخت چند فولدر در چند مسیر مختلف با لیست مسیرها و نام‌ها به صورت ای‌سینک
///// </summary>
///// <param name="directories">List of parent directories. / لیست مسیرهای والد</param>
///// <param name="folderNames">List of folder names. / لیست نام فولدرها</param>
///// <remarks>
///// The lists must be the same length. Each folder in folderNames is created inside the corresponding directory.
///// طول دو لیست باید برابر باشد.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderAsync(List<string> directories, List<string> folderNames, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default) => FolderOpsCore.MakeFolder_Async(directories, folderNames, PL, token);

///// <summary>
///// English : Creates multiple folders inside a single directory using a variable number of folder names asynchronously.
///// Farsi  : ساخت چند فولدر داخل یک مسیر مشخص با چند نام فولدر متغیر به صورت ای‌سینک
///// </summary>
///// <param name="directory">The parent directory. / مسیر والد</param>
///// <param name="folderNames">An array of folder names. / آرایه‌ای از نام فولدرها</param>
///// <remarks>
///// Example: CreateFolderAsync("C:\\Temp", "A", "B", "C").
///// مناسب زمانی که می‌خواهید چند فولدر داخل یک مسیر بسازید.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderAsync(string directory, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] folderNames) => FolderOpsCore.MakeFolder_Async(new List<string> { directory }, folderNames.ToList(), PL, token);

///// <summary>
///// English : Creates the same folder inside multiple parent directories asynchronously.
///// Farsi  : ساخت یک فولدر مشابه در چند مسیر والد به صورت ای‌سینک
///// </summary>
///// <param name="folderName">The folder name to create. / نام فولدر</param>
///// <param name="directories">An array of parent directories. / آرایه‌ای از مسیرهای والد</param>
///// <remarks>
///// Example: CreateFolderInMultiDirectoriesAsync("Logs", "C:\\App1", "D:\\App2")
///// Creates "Logs" folder in all specified directories, only if it does not already exist.
///// مناسب زمانی که می‌خواهید یک فولدر مشابه در چند مسیر بسازید.
///// </remarks>
///// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />
//public static Task CreateFolderInMultiDirectoriesAsync(string folderName, ePriorityLevel PL = ePriorityLevel.MidLevel, CancellationToken token = default, params string[] directories) => FolderOpsCore.MakeFolder_Async(directories.ToList(), new List<string> { folderName }, PL, token);