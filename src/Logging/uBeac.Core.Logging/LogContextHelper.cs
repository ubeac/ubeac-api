using System.Diagnostics;
using System.Reflection;
using Serilog.Context;
namespace uBeac.Logging;

public static class LogContextHelper
{
    public static void PushDefaultProperties()
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        var process = Process.GetCurrentProcess();

        GlobalLogContext.PushProperty("App_AssemblyName", assembly.GetName().Name);
        GlobalLogContext.PushProperty("App_AssemblyVersion", assembly.GetName()?.Version?.ToString());
        GlobalLogContext.PushProperty("App_EnvironmentName", GetEnvironmentName());
        GlobalLogContext.PushProperty("App_EnvironmentUserName", GetEnvironmentUserName());
        GlobalLogContext.PushProperty("App_EnvironmentUserInteraction", Environment.UserInteractive);
        GlobalLogContext.PushProperty("App_DebugMode", Debugger.IsAttached);
        GlobalLogContext.PushProperty("App_MachineName", Environment.MachineName);
        GlobalLogContext.PushProperty("App_MemoryUsage", GC.GetTotalMemory(false));
        GlobalLogContext.PushProperty("App_ProcessId", process.Id);
        GlobalLogContext.PushProperty("App_ProcessName", process.ProcessName);
        GlobalLogContext.PushProperty("App_ProcessStartTime", process.StartTime);
        GlobalLogContext.PushProperty("App_ThreadId", Environment.CurrentManagedThreadId);
        GlobalLogContext.PushProperty("App_ThreadName", Thread.CurrentThread.Name);
    }

    private static string GetEnvironmentName()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (string.IsNullOrWhiteSpace(environmentName)) environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        if (string.IsNullOrWhiteSpace(environmentName)) environmentName = "Production";

        return environmentName;
    }

    private static string GetEnvironmentUserName()
    {
        var userDomainName = Environment.GetEnvironmentVariable("USERDOMAIN");
        var userName = Environment.GetEnvironmentVariable("USERNAME");
        return !string.IsNullOrWhiteSpace(userDomainName) ? $@"{userDomainName}\{userName}" : userName;
    }
}