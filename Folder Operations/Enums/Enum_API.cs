namespace NeraTools
{
    public static partial class FolderOps
    {
        /// <summary>
        /// --> Overwrite
        /// English : Overwrite existing files in the destination ( if file exist ! ).
        /// Farsi  : بازنویسی فایل‌های موجود در مسیر مقصد ( اگر فایل موجود بود ).
        ///
        /// --> BackupBeforeOverwrite
        /// English : Backup existing files before overwriting.
        /// Farsi  : گرفتن بکاپ از فایل‌های موجود قبل از بازنویسی
        ///
        /// --> SkipExisting
        /// English : Skip copying files that already exist.
        /// Farsi  : نادیده گرفتن فایل‌های موجود
        ///
        /// --> Logger
        /// English : Enable logging for the operation.
        /// Farsi  : فعال کردن ثبت گزارش (لاگ) برای عملیات
        ///
        /// --> PreserveAttributes
        /// English : Keep file/folder attributes (readonly, hidden, etc.)
        /// Farsi  : حفظ ویژگی‌های فایل/فولدر (مثل readonly، hidden و …)
        ///
        /// --> IgnoreErrors
        /// English : Continue copying even if some errors occur.
        /// Farsi  : ادامه عملیات کپی حتی در صورت بروز خطا در برخی فایل‌ها
        ///
        /// --> OverwriteIfNewer
        /// English : Only overwrite if source file is newer than destination.
        /// Farsi  : بازنویسی فقط در صورتی که فایل مبدا جدیدتر باشد
        ///
        /// --> UseTempFiles
        /// English : Copy via temporary files to prevent partial copies.
        /// Farsi  : استفاده از فایل موقت برای جلوگیری از کپی ناقص
        /// </summary>
        public enum FolderTransfomOptions
        {
            /// <summary>
            /// English : Overwrite existing files in the destination ( if file exist ! ).
            /// Farsi  : بازنویسی فایل‌های موجود در مسیر مقصد ( اگر فایل موجود بود ).
            /// </summary>
            Overwrite,

            /// <summary>
            /// English : Backup existing files before overwriting.
            /// Farsi  : گرفتن بکاپ از فایل‌های موجود قبل از بازنویسی
            /// </summary>
            BackupBeforeOverwrite,

            /// <summary>
            /// English : Skip copying files that already exist.
            /// Farsi  : نادیده گرفتن فایل‌های موجود
            /// </summary>
            SkipExisting,

            /// <summary>
            /// English : Enable logging for the operation.
            /// Farsi  : فعال کردن ثبت گزارش (لاگ) برای عملیات
            /// </summary>
            Logger,

            /// <summary>
            /// English : Keep file/folder attributes (readonly, hidden, etc.)
            /// Farsi  : حفظ ویژگی‌های فایل/فولدر (مثل readonly، hidden و …)
            /// </summary>
            PreserveAttributes,

            /// <summary>
            /// English : Continue copying even if some errors occur.
            /// Farsi  : ادامه عملیات کپی حتی در صورت بروز خطا در برخی فایل‌ها
            /// </summary>
            IgnoreErrors,

            /// <summary>
            /// English : Only overwrite if source file is newer than destination.
            /// Farsi  : بازنویسی فقط در صورتی که فایل مبدا جدیدتر باشد
            /// </summary>
            OverwriteIfNewer,

            /// <summary>
            /// English : Copy via temporary files to prevent partial copies.
            /// Farsi  : استفاده از فایل موقت برای جلوگیری از کپی ناقص
            /// </summary>
            UseTempFiles
        }

        /// <summary>
        /// Specifies options for controlling folder deletion operations, such as backup, error handling, logging, and
        /// overwrite behavior.
        /// </summary>
        /// <remarks>Use the FolderDeleteOptions enumeration to customize how folders and their contents
        /// are deleted. Options can be combined to enable features like backing up files before deletion, skipping
        /// files that do not exist, logging the operation, continuing deletion despite errors, or overwriting files
        /// only if the source is newer. These options allow for flexible and safe folder management during delete
        /// operations.</remarks>
        public enum FolderDeleteOptions
        {
            /// <summary>
            /// English : Backup existing files before Deleate.
            /// Farsi  : گرفتن بکاپ از فایل‌های موجود قبل از حذف کردن
            /// </summary>
            BackupBeforeDeleate,

            /// <summary>
            /// English : Skip copying files that already exist.
            /// Farsi  : نادیده گرفتن فایل‌های موجود
            /// </summary>
            SkipIfNotExisting,

            /// <summary>
            /// English : Enable logging for the operation.
            /// Farsi  : فعال کردن ثبت گزارش (لاگ) برای عملیات
            /// </summary>
            Logger,

            /// <summary>
            /// English : Continue Deleting even if some errors occur.
            /// Farsi  : ادامه عملیات حذف حتی در صورت بروز خطا در برخی فایل‌ها
            /// </summary>
            IgnoreErrors,

            /// <summary>
            /// English : Only overwrite if source file is newer than destination.
            /// Farsi  : بازنویسی فقط در صورتی که فایل مبدا جدیدتر باشد
            /// </summary>
            OverwriteIfNewer,

            /// <summary>
            /// English : Recursively delete all subfolders and files within the specified folder.
            /// فارسی  : حذف بازگشتی تمام زیرپوشه‌ها و فایل‌های داخل پوشه مشخص شده.
            /// </summary>
            recursive,

            /// <summary>
            /// Retry deleting if folder is in use, wait a few seconds and try again.
            /// فارسی: اگر فولدر در حال استفاده بود، چند ثانیه بعد دوباره تلاش کند
            /// </summary>
            RetryIfInUse
        }
    }
}