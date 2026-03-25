namespace NeraTools.LogManager
{
    public static partial class Logger
    {
        internal static void logForThisTool(string message, eLogType type = eLogType.Info, params eLogRecordMode[] mods)
           => Logger_Core.Log_Core(message, type, eLogCategory.FrameworkLog, mods);

        public static void log(string message, eLogType type = eLogType.Info, params eLogRecordMode[] mods)
           => Logger_Core.Log_Core(message, type, eLogCategory.UsearApplicationLog, mods);

        public static void writeLogInConsole(bool isEnable = true)
         => RunModsDTO.isConsole = isEnable ? true : false;

        public static void writeLogInUi(bool isEnable = true)
         => RunModsDTO.isUI = isEnable ? true : false;

        public static void writeLogInJsonFile(bool isEnable = true, string saveLocation = null, string fileName = "Log")
        {
            if (isEnable) { RunModsDTO.isJson = true; RunModsDTO.jsonFileLocation = saveLocation; RunModsDTO.jsonFileName = fileName; }
            else { RunModsDTO.isJson = false; RunModsDTO.jsonFileLocation = string.Empty; RunModsDTO.jsonFileName = string.Empty; }
        }

        public static void writeLogInTextFile(bool isEnable = true, string saveLocation = null, string fileName = "Log")
        {
            if (isEnable) { RunModsDTO.isTxt = true; RunModsDTO.txtFileLocation = saveLocation; RunModsDTO.txtFileName = fileName; }
            else { RunModsDTO.isTxt = false; RunModsDTO.txtFileLocation = string.Empty; RunModsDTO.txtFileName = string.Empty; }
        }

        /// <summary>
        /// Enables or disables the automatic offline mode feature.
        /// TIP : Default is true in Logger !
        /// </summary>
        /// <param name="isEnable">Specifies whether automatic offline mode should be enabled. Set to <see langword="true"/> to enable;
        /// otherwise, set to <see langword="false"/>.</param>
        public static void AutoOfflinetion(bool isEnable = true)
        {
            if (isEnable) Logger_Core.autooffline = true;
            else Logger_Core.autooffline = false;
        }

        public static void DisableLoggerSystem() => Logger_Core.globalCTS.Cancel();
    }
}