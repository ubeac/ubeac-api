using Microsoft.Extensions.DependencyInjection;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using uBeac;

namespace Serilog;

public class AppContextEnricher : ILogEventEnricher
{
    protected readonly IServiceCollection Services;

    public AppContextEnricher(IServiceCollection services)
    {
        Services = services;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var context = Services.BuildServiceProvider().GetRequiredService<IApplicationContext>();
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TraceId", context.TraceId));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("SessionId", context.SessionId));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserName", context.UserName));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserIp", context.UserIp?.ToString()));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Language", context.Language));
    }
}

public static class AppContextEnricherExtensions
{
    public static LoggerConfiguration WithAppContext(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceCollection services)
        => enrichmentConfiguration.With(new AppContextEnricher(services));
}