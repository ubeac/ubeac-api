using Serilog;
using Serilog.Formatting.Json;
using uBeac.Core.Logging;

namespace uBeac.Logging.MongoDB
{
    public static class Extensions
    {
        public static ILoggingRegistration WriteToMongoDb(this ILoggingRegistration logging, MongoDbLogOptions options)
        {
            var jsonFormatter = new NormalJsonFormatter();

            logging.Configuration
                .WriteTo.Logger(lc => lc.Filter.With(new ErrorLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.ErrorCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new FatalLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.FatalCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new InformationLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.InformationCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new DebugLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.DebugCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new VerboseLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.VerboseCollection))

                .WriteTo.Logger(lc => lc.Filter.With(new WarningLogEvent())
                    .WriteTo.MongoDB(mongoDBJsonFormatter: jsonFormatter, databaseUrl: options.ConnectionString, collectionName: options.WarningCollection));

            return logging;
        }
    }

}
