namespace NeraXTools
{
    public static partial class FolderOps
    {
        // =========================
        // Create Folder Methods - Sync
        // =========================
        /// <summary>
        /// English : Move a single folder to a destination path.
        /// Farsi  : کات کردن یک فولدر به مسیر مقصد
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolder(string sourcePath, string destinationPath, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : moving multiple folders to a single destination.
        /// Farsi  : کات کردن چند فولدر به یک مسیر مقصد
        /// </summary>
        /// <param name="sourcePaths">List of source folders / لیست فولدرهای مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolders(List<string> sourcePaths, string destinationPath, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(sourcePaths, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : /// Persian: Move multiple folders to multiple destination paths [based on list order];
        /// Farsi  : /// فارسی: کات کردن چند پوشه در چندین مسیر مقصد  [به اساس ترتیب لیست]؛
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFoldersToMultiDestinations(List<string> sourcePaths, List<string> destinationPaths, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(sourcePaths, destinationPaths, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : moving a single folder to multiple destination paths.
        /// Farsi  : کات کردن یک فولدر به چند مسیر مقصد
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolderToMultipleDestinations(string sourcePath, List<string> destinationPaths, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, destinationPaths, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folder with combined filters (Name, Extension, Size, Date, Attributes)
        /// Farsi  : کات کردن فولدر با فیلتر ترکیبی (نام، پسوند، سایز، تاریخ، ویژگی‌ها)
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolder(string sourcePath, string destinationPath, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : moving multiple folders to a single destination (Name, Extension, Size, Date, Attributes)
        /// Farsi  : کات کردن چند فولدر به یک مسیر مقصد با فیلتر ترکیبی (نام، پسوند، سایز، تاریخ، ویژگی‌ها)
        /// </summary>
        /// <param name="sourcePaths">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolders(List<string> sourcePaths, string destinationPath, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(sourcePaths, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move multiple folders to multiple destinations with combined filters
        /// Farsi  : کات کردن چند فولدر به چند مسیر مقصد با فیلتر ترکیبی
        /// </summary>
        /// <param name="sourcePaths">List of source folders / لیست فولدرهای مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFoldersToMultiDestinations(List<string> sourcePaths, List<string> destinationPaths, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(sourcePaths, destinationPaths, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move a single folder to multiple destinations with combined filters
        /// Farsi  : کات کردن یک فولدر به چند مسیر مقصد با فیلتر ترکیبی
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolderToMultipleDestinations(string sourcePath, List<string> destinationPaths, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, destinationPaths, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move folders within the address based on name filter
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر نام
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Filter pattern / الگوی فیلتر نام</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolder_WithFilterNames(string sourcePath, string destinationPath, string nameFilter, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on Size_Renge filter
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر محدودیت سایز
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="minSize">Minimum file size in bytes / حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Maximum file size in bytes / حداکثر حجم فایل به بایت</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolderWithSizeFilter(string sourcePath, string destinationPath, long minSize, long maxSize = long.MaxValue, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, minSize, maxSize, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on extension filter
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر پسوند
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="extensions">Allowed extensions / پسوندهای مجاز</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolderWithExtensionFilter(string sourcePath, string destinationPath, List<string> extensions, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, extensions, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on Time_Renge filter
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر محدودیت زمانی
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="startDate">Start date / تاریخ شروع</param>
        /// <param name="endDate">End date / تاریخ پایان</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        public static void MoveFolderWith_CreationDateRange(string sourcePath, string destinationPath, DateTime startDate, DateTime endDate, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Sync(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, startDate, endDate, null, null, null, null, options);

        // =========================
        //  Folder Move Methods - Async
        // =========================

        /// <summary>
        /// English : moving a single folder to a destination path by Async.
        /// Farsi  : کات کردن یک فولدر به مسیر مقصد  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolder_Async(string sourcePath, string destinationPath, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : moving multiple folders to a single destinationby Async.
        /// Farsi  : کات کردن چند فولدر به یک مسیر مقصد  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePaths">List of source folders / لیست فولدرهای مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolders_Async(List<string> sourcePaths, string destinationPath, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(sourcePaths, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : /// Persian: Move multiple folders to multiple destination paths by Async [based on list order];
        /// Farsi  : /// فارسی: کات کردن چند پوشه در چندین مسیر مقصد  [به اساس ترتیب لیست]؛  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFoldersToMultiDestinations_Async(List<string> sourcePaths, List<string> destinationPaths, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(sourcePaths, destinationPaths, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : moving a single folder to multiple destination paths by Async.
        /// Farsi  : کات کردن یک فولدر به چند مسیر مقصد  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolderToMultipleDestinations_Async(string sourcePath, List<string> destinationPaths, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, destinationPaths, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folder with combined filters  (Name, Extension, Size, Date, Attributes) by Async
        /// Farsi  : کات کردن فولدر با فیلتر ترکیبی (نام، پسوند، سایز، تاریخ، ویژگی‌ها)  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolder_Async(string sourcePath, string destinationPath, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : moving multiple folders to a single destination (Name, Extension, Size, Date, Attributes) by Async
        /// Farsi  : کات کردن چند فولدر به یک مسیر مقصد با فیلتر ترکیبی (نام، پسوند، سایز، تاریخ، ویژگی‌ها)  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePaths">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolders_Async(List<string> sourcePaths, string destinationPath, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(sourcePaths, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move multiple folders to multiple destinations with combined filters by Async
        /// Farsi  : کات کردن چند فولدر به چند مسیر مقصد با فیلتر ترکیبی  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePaths">List of source folders / لیست فولدرهای مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFoldersToMultiDestinations_Async(List<string> sourcePaths, List<string> destinationPaths, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(sourcePaths, destinationPaths, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move a single folder to multiple destinations with combined filters by Async
        /// Farsi  : کات کردن یک فولدر به چند مسیر مقصد با فیلتر ترکیبی  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPaths">List of destination paths / لیست مسیرهای مقصد</param>
        /// <param name="nameFilter">Optional: filter by name pattern / اختیاری: فیلتر بر اساس نام</param>
        /// <param name="extensions">Optional: allowed extensions / اختیاری: پسوندهای مجاز</param>
        /// <param name="minSize">Optional: minimum file size in bytes / اختیاری: حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Optional: maximum file size in bytes / اختیاری: حداکثر حجم فایل به بایت</param>
        /// <param name="Creation_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="Creation_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="LastWrite_startDate">Optional: start date / اختیاری: تاریخ شروع</param>
        /// <param name="LastWrite_endDate">Optional: end date / اختیاری: تاریخ پایان</param>
        /// <param name="attributes">Optional: file/folder attributes filter / اختیاری: فیلتر ویژگی‌های فایل/فولدر</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolderToMultipleDestinations_Async(string sourcePath, List<string> destinationPaths, List<string> startWith, List<string> nameFilter = null, List<string> extensions = null, long minSize = 0, long maxSize = long.MaxValue, DateTime? Creation_startDate = null, DateTime? Creation_endDate = null, DateTime? LastWrite_startDate = null, DateTime? LastWrite_endDate = null, FileAttributes? attributes = null, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, destinationPaths, FolderOpsCore.Transform_Options.Copy, nameFilter, extensions, minSize, maxSize, Creation_startDate, Creation_endDate, LastWrite_startDate, LastWrite_endDate, startWith, attributes, options);

        /// <summary>
        /// English : Move folders within the address based on name filter by Async
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر نام  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="nameFilter">Filter pattern / الگوی فیلتر نام</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolder_WithFilterNames_Async(string sourcePath, string destinationPath, string nameFilter, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on Size_Renge filter by Async
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر محدودیت سایز  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="minSize">Minimum file size in bytes / حداقل حجم فایل به بایت</param>
        /// <param name="maxSize">Maximum file size in bytes / حداکثر حجم فایل به بایت</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolderWithSizeFilter_Async(string sourcePath, string destinationPath, long minSize, long maxSize = long.MaxValue, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, minSize, maxSize, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on extension filter by Async
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر پسوند  به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="extensions">Allowed extensions / پسوندهای مجاز</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolderWithExtensionFilter_Async(string sourcePath, string destinationPath, List<string> extensions, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, extensions, 0, long.MaxValue, null, null, null, null, null, null, options);

        /// <summary>
        /// English : Move folders within the address based on Time_Renge filter by Async
        /// Farsi  : کات کردن فولدرهای داخل ادرس بر اساس فیلتر محدودیت زمانی به واسطه ایسینک
        /// </summary>
        /// <param name="sourcePath">Source folder path / مسیر فولدر مبدا</param>
        /// <param name="destinationPath">Destination folder path / مسیر فولدر مقصد</param>
        /// <param name="startDate">Start date / تاریخ شروع</param>
        /// <param name="endDate">End date / تاریخ پایان</param>
        /// <param name="options">
        /// English: Specify folder Move options like Overwrite, BackupBeforeOverwrite, SkipExisting, Logger.
        /// Farsi : گزینه‌های کات کردن فولدر مثل بازنویسی، بکاپ قبل از بازنویسی، نادیده گرفتن فایل‌های موجود، و فعال کردن لاگ
        /// </param>
        /// <include file='CommonRemarks.xml' path='doc/members/member[@name="T:CommonRemarks.ParallelAsyncWarning"]/*' />

        public static Task MoveFolderWith_CreationDateRange_Async(string sourcePath, string destinationPath, DateTime startDate, DateTime endDate, params FolderTransfomOptions[] options) => FolderOpsCore.Move_Async(new List<string> { sourcePath }, new List<string> { destinationPath }, FolderOpsCore.Transform_Options.Copy, null, null, 0, long.MaxValue, startDate, endDate, null, null, null, null, options);
    } // end of Folder_Ops class
} // end of NeraXTools namespace