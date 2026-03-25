namespace NeraTools.LogManager
{
    public class LogDTO
    {
        public string msg { get; set; }
        public eLogType Type { get; set; }
        public eLogRecordMode[] Mods { get; set; } = Array.Empty<eLogRecordMode>();
        public eLogCategory Category { get; set; }
    }

    public static class RunModsDTO
    {
        public static bool isConsole { get; set; }
        public static bool isUI { get; set; }
        public static bool isJson { get; set; }
        public static string jsonFileLocation { get; set; }
        public static string jsonFileName { get; set; }
        public static bool isTxt { get; set; }
        public static string txtFileLocation { get; set; }
        public static string txtFileName { get; set; }
        public static bool isAllModes { get; set; }
    }
}