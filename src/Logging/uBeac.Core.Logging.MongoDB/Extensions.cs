using Serilog;

namespace uBeac.Logging.MongoDB
{
    public static class Extensions
    {
        public static LoggerConfiguration WriteToMongoDB(this LoggerConfiguration loggerConfiguration, string connectionString, MongoDBLogSetting mongoDBLogSetting = default)
        {
            mongoDBLogSetting ??= new MongoDBLogSetting();

            loggerConfiguration
                .WriteTo.Logger(lc => lc.Filter.With(new ErrorLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.ErrorCollection))
                .WriteTo.Logger(lc => lc.Filter.With(new FatalLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.FatalCollection))
                .WriteTo.Logger(lc => lc.Filter.With(new InformationLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.InformationCollection))
                .WriteTo.Logger(lc => lc.Filter.With(new DebugLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.DebugCollection))
                .WriteTo.Logger(lc => lc.Filter.With(new VerboseLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.VerboseCollection))
                .WriteTo.Logger(lc => lc.Filter.With(new WarningLogEvent()).WriteTo.MongoDB(connectionString, collectionName: mongoDBLogSetting.WarningCollection));

            return loggerConfiguration;
        }
    }

}
