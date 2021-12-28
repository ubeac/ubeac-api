namespace uBeac.Logging.MongoDB
{
    public class MongoDBLogSetting
    {
        public string DebugCollection { get; set; } = "Debug";
        public string ErrorCollection { get; set; } = "Error";
        public string VerboseCollection { get; set; } = "Verbose";
        public string FatalCollection { get; set; } = "Fatal";
        public string WarningCollection { get; set; } = "Warning";
        public string InformationCollection { get; set; } = "Information";
    }
}
