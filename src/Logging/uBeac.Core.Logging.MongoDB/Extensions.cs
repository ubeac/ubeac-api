using Serilog;
using Serilog.Formatting.Json;

namespace uBeac.Logging.MongoDB
{
    public static class Extensions
    {
        public static LoggerConfiguration WriteToMongoDB(this LoggerConfiguration loggerConfiguration, string connectionString, MongoDBLogSetting mongoDBLogSetting = null)
        {
            mongoDBLogSetting ??= new MongoDBLogSetting();
            var jsonFormatter = new NormalJsonFormatter();

            loggerConfiguration
                .WriteTo.Logger(lc => lc.Filter.With(new ErrorLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.ErrorCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new FatalLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.FatalCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new InformationLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.InformationCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new DebugLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.DebugCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new VerboseLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.VerboseCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new WarningLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: mongoDBLogSetting.WarningCollection));

            return loggerConfiguration;
        }
    }

}
