using Serilog;
using Serilog.Configuration;
using Serilog.Exceptions;
using System;

namespace uBeac.Logging
{
    public static class AppLogEnricherConfigurationExtensions
    {
        public static LoggerConfiguration WithAppEnricher(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<AppLogEnricher>().Enrich.WithExceptionDetails();
        }
    }
}
