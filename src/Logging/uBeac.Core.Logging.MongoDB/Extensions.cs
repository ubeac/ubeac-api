using Serilog;
using Serilog.Formatting.Json;

namespace uBeac.Logging.MongoDB
{
    public static class Extensions
    {
        public static LoggerConfiguration WriteToMongoDb(this LoggerConfiguration loggerConfiguration, string connectionString, MongoDbLogOptions options)
        {
            var jsonFormatter = new NormalJsonFormatter();

            loggerConfiguration
                .WriteTo.Logger(lc => lc.Filter.With(new ErrorLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.ErrorCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new FatalLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.FatalCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new InformationLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.InformationCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new DebugLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.DebugCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new VerboseLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.VerboseCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new WarningLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: connectionString, collectionName: options.WarningCollection));

            return loggerConfiguration;
        }
    }

}
