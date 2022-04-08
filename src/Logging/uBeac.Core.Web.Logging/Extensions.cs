using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static LoggerConfiguration AddApiLogging(this LoggerConfiguration configuration,
            IServiceCollection services, string seqUrl)
        {
            configuration
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Warning)
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
                .WriteTo.Seq(seqUrl);

            return configuration;
        }
    }
}