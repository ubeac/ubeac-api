using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog;

public class RemoveRequestPropsEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("RequestId");
        logEvent.RemovePropertyIfPresent("RequestPath");
    }
}

public static class RemoveRequestPropsEnricherExtensions
{
    public static LoggerConfiguration WithRemoveRequestProperties(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        => enrichmentConfiguration.With(new RemoveRequestPropsEnricher());
}