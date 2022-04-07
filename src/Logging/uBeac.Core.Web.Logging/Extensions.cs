using Serilog;
using Serilog.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static LoggerConfiguration AddApiLogging(this LoggerConfiguration configuration, IServiceCollection services, string seqUrl)
        {
            configuration
                .Enrich.FromGlobalLogContext()
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
                .WriteTo.Seq(seqUrl);

            return configuration;
        }
    }
}