using Serilog;
using Serilog.Exceptions;
using uBeac.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static LoggerConfiguration AddApiLogging(this LoggerConfiguration configuration)
        {
            configuration
                .Enrich.FromGlobalLogContext()
                .Enrich.WithExceptionDetails();

            LogContextHelper.PushDefaultProperties();

            return configuration;
        }
    }
}
