namespace NeraTools.TaskManager.Apps
{
    internal static class Appslocations
    {
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
        internal static readonly string TaskMonitorPanel_x64 = Path.Combine(basePath, "TaskManager", "Apps", "TaskMonitorPanel_x64.exe");
        internal static readonly string TaskMonitorPanel_x86 = Path.Combine(basePath, "TaskManager", "Apps", "TaskMonitorPanel_x86.exe");
    }
}