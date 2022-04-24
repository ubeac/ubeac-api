using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;
using uBeac.Web;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static LoggerConfiguration AddHttpLogging(this LoggerConfiguration configuration, IServiceCollection services)
        {
            configuration
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
                .Enrich.WithAppContext(services)
                .Enrich.WithRemoveRequestProperties();

            return configuration;
        }
    }
}