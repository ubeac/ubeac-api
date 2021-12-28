using Serilog.Core;
using Serilog.Events;

namespace uBeac.Logging
{
    public class FatalLogEvent : ILogEventFilter
    {
        public bool IsEnabled(LogEvent logEvent)
        {
            return logEvent.Level == LogEventLevel.Fatal;
        }
    }
}
