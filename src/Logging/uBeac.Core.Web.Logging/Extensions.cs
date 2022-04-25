using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;
using uBeac.Core.Logging;
using uBeac.Web;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static ILoggingRegistration AddHttpLogging(this ILoggingBuilder logging)
        {
            var configuration = new LoggerConfiguration()
                .Filter.ByIncludingOnly(Matching.FromSource<HttpLoggingMiddleware>())
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .Enrich.WithAssemblyInformationalVersion()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.WithMemoryUsage()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithCorrelationId()
                .Enrich.WithAppContext(logging.Services)
                .Enrich.WithRemoveRequestProperties();

            return new LoggingRegistration(logging, configuration);
        }

        public static ILoggingRegistration UsingSerilog(this ILoggingRegistration logging)
        {
            var logger = logging.Configuration.CreateLogger();
            logging.Builder.AddSerilog(logger);
            return logging;
        }
    }
}