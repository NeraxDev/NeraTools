namespace NeraXTools.TaskManager
{
    /// <summary>
    /// CPU resource usage percentage options.
    /// گزینه‌های درصد استفاده از منابع CPU.
    /// </summary>
    public enum eThreadUsagePercent
    {
        _10Percent = 10,
        _20Percent = 20,
        _30Percent = 30,
        _40Percent = 40,
        _50Percent = 50,
        _60Percent = 60,
        _70Percent = 70,
        _80Percent = 80,
        _90Percent = 90,
        _100Percent = 100
    }

    /// <summary>
    /// Task execution priority levels.
    /// Higher priority tasks execute before lower priority tasks.
    ///
    /// سطح اولویت اجرای تسک‌ها.
    /// تسک‌های با اولویت بالاتر قبل از پایین‌تر اجرا می‌شوند.
    /// </summary>
    public enum ePriorityLevel
    {
        /// <summary>Highest priority tasks</summary>
        HighLevel,

        /// <summary>Medium priority tasks</summary>
        MidLevel,

        /// <summary>Low priority tasks</summary>
        LowLevel,

        /// <summary>Tasks that should execute at end of queue</summary>
        EndLevel,

        /// <summary>Tasks that should execute at beginning of queue</summary>
        StartLevel
    }

    public enum eFrameworkPriorityLevel
    {
        /// <summary> use in special Tools in Framework</summary>
        FrameworkLevel,

        /// <summary>Highest priority tasks</summary>
        HighLevel,

        /// <summary>Medium priority tasks</summary>
        MidLevel,

        /// <summary>Low priority tasks</summary>
        LowLevel,

        /// <summary>Tasks that should execute at end of queue</summary>
        EndLevel,

        /// <summary>Tasks that should execute at beginning of queue</summary>
        StartLevel
    }

    public enum eApplicationState
    {
        Running,
        Disabled
    }
}