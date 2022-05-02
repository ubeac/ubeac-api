using Microsoft.Extensions.Logging;
using Serilog;

namespace uBeac.Core.Logging;

public interface ILoggingRegistration
{
    public ILoggingBuilder Builder { get; }
    public LoggerConfiguration Configuration { get; }
}

public class LoggingRegistration : ILoggingRegistration
{
    public LoggingRegistration(ILoggingBuilder builder, LoggerConfiguration configuration)
    {
        Builder = builder;
        Configuration = configuration;
    }

    public ILoggingBuilder Builder { get; }
    public LoggerConfiguration Configuration { get; }
}