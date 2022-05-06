namespace uBeac.Logging.MongoDB
{
    public class MongoDbLogOptions
    {
        public string ConnectionString { get; set; }
        public string DebugCollection { get; set; }
        public string ErrorCollection { get; set; }
        public string VerboseCollection { get; set; }
        public string FatalCollection { get; set; }
        public string WarningCollection { get; set; }
        public string InformationCollection { get; set; }
    }
}
