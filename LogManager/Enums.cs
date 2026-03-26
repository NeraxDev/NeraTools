namespace NeraXTools.LogManager
{
    public enum eLogType
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Critical = 3,
        Debug = 4,
        Exception = 5
    }

    public enum eLogCategory
    {
        FrameworkLog,
        UsearApplicationLog
    }

    public enum eLogRecordMode
    {
        Console,
        UI,
        Json,
        Txt,
        AllModes
    }

    public enum eLogTheme
    {
        Classic,
        Neon,
        Dark,
        Warm,
        Soft
    }

    public enum eLogThemeColor
    {
        // Classic
        Black,

        White,
        Gray,

        // Info Style
        Blue,

        LightBlue,
        Cyan,

        // Warning Style
        Yellow,

        Orange,

        // Error Style
        Red,

        DarkRed,

        // Success Style
        Green,

        Lime,

        // Dark Mode Friendly
        Purple,

        Magenta,
        Pink,

        // Special
        Gold,

        Silver,
        Brown,

        //mesageColor
    }
}