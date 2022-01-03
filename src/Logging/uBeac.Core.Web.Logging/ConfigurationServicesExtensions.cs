using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using uBeac.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationServicesExtensions
    {
        public static LoggerConfiguration AddApiLog(this LoggerConfiguration configuration)
        {
                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithAppEnricher();
                    //.Enrich.WithAssemblyName()
                    //.Enrich.WithAssemblyVersion()
                    //.Enrich.WithThreadId()
                    //.Enrich.WithMachineName()
                    //.Enrich.WithEnvironmentUserName()
                    //.Enrich.WithProcessId()
                    //.Enrich.WithProcessName()

            return configuration;

        }

    }

}
