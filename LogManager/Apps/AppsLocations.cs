namespace NeraTools.LogManager.Apps
{
    internal class AppsLocations
    {
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
        internal static readonly string TaskMonitorPanel_x64 = Path.Combine(basePath, "LogManager", "Apps", "LogMonitorPanal_x64.exe");
        internal static readonly string TaskMonitorPanel_x86 = Path.Combine(basePath, "LogManager", "Apps", "LogMonitorPanal_x86.exe");
    }
}