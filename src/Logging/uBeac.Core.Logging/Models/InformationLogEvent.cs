using Serilog.Core;
using Serilog.Events;

namespace uBeac.Logging
{
    public class InformationLogEvent : ILogEventFilter
    {
        public bool IsEnabled(LogEvent logEvent)
        {
            return logEvent.Level == LogEventLevel.Information;
        }
    }
}
