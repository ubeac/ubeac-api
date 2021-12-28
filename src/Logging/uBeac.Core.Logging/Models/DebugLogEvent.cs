using Serilog.Core;
using Serilog.Events;

namespace uBeac.Logging
{
    public class DebugLogEvent : ILogEventFilter
    {
        public bool IsEnabled(LogEvent logEvent)
        {
            return logEvent.Level == LogEventLevel.Debug;
        }
    }
}
