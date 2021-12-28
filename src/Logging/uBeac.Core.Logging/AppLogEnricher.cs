using Serilog.Core;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace uBeac.Logging
{
    public class AppLogEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;
        public const string ApplicationLogPropertyName = "Application";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var process = Process.GetCurrentProcess();

            var log = new AppLog
            {
                AssemblyName = assembly.GetName().Name,
                AssemblyVersion = assembly.GetName().Version.ToString(),
                EnvironmentName = GetEnvironmentName(),
                EnvironmentUserName = GetEnvironmentUserName(),
                EnvironmentUserInteraction = Environment.UserInteractive,
                DebugMode = Debugger.IsAttached,
                MachineName = Environment.MachineName,
                MemoryUsage = GC.GetTotalMemory(false),
                ProcessId = process.Id,
                ProcessName = process.ProcessName,
                ProcessStartTime = process.StartTime,
                ThreadId = Environment.CurrentManagedThreadId,
                ThreadName = Thread.CurrentThread.Name
            };

            _cachedProperty ??= propertyFactory.CreateProperty(ApplicationLogPropertyName, log, true);
            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }

        private static string GetEnvironmentName()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(environmentName))
                environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(environmentName))
                environmentName = "Production";

            return environmentName;

        }

        private static string GetEnvironmentUserName()
        {
            var userDomainName = Environment.GetEnvironmentVariable("USERDOMAIN");
            var userName = Environment.GetEnvironmentVariable("USERNAME");
            return !string.IsNullOrWhiteSpace(userDomainName) ? $@"{userDomainName}\{userName}" : userName;
        }

    }
}
