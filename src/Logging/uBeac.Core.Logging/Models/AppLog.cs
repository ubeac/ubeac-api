namespace uBeac.Logging
{
    public class AppLog
    {
        public int ThreadId { get; set; } = 0;
        public string ThreadName { get; set; } = string.Empty;
        public int ProcessId { get; set; } = 0;
        public string ProcessName { get; set; } = string.Empty;
        public DateTime ProcessStartTime { get; set; } = DateTime.Now;
        public long MemoryUsage { get; set; } = 0;
        public string EnvironmentName { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public string EnvironmentUserName { get; set; } = string.Empty;
        public bool EnvironmentUserInteraction { get; set; } = false;
        public bool DebugMode { get; set; } = false;
        public string AssemblyName { get; set; } = string.Empty;
        public string AssemblyVersion { get; set; } = string.Empty;
    }
}
