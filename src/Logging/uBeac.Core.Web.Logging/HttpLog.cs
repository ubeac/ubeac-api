using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace uBeac.Web.Logging;

public class HttpLog : Entity
{
    public HttpLog()
    {
        var dateTime = DateTime.Now;
        var assembly = Assembly.GetEntryAssembly();
        var assemblyName = assembly.GetName();
        var process = Process.GetCurrentProcess();
        var thread = Thread.CurrentThread;

        Time = dateTime;
        AssemblyName = assemblyName.Name;
        AssemblyVersion = assemblyName.Version?.ToString();
        ProcessId = process.Id;
        ProcessName = process.ProcessName;
        ThreadId = thread.ManagedThreadId;
        MemoryUsage = process.PrivateMemorySize64;
        MachineName = Environment.MachineName;
        EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        EnvironmentUserName = Environment.UserName;
    }

    public HttpLog(HttpLog log)
    {
        Time = log.Time;
        Request = log.Request;
        Response = log.Response;
        StatusCode = log.StatusCode;
        Duration = log.Duration;
        AssemblyName = log.AssemblyName;
        AssemblyVersion = log.AssemblyVersion;
        ProcessId = log.ProcessId;
        ProcessName = log.ProcessName;
        ThreadId = log.ThreadId;
        MemoryUsage = log.MemoryUsage;
        MachineName = log.MachineName;
        EnvironmentName = log.EnvironmentName;
        EnvironmentUserName = log.EnvironmentUserName;
        Context = log.Context;
        Exception = log.Exception;
    }

    public DateTime Time { get; set; }

    public HttpRequestLog Request { get; set; }
    public HttpResponseLog Response { get; set; }

    public int StatusCode { get; set; }
    public long Duration { get; set; }

    public string AssemblyName { get; set; }
    public string AssemblyVersion { get; set; }

    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public int ThreadId { get; set; }
    public long MemoryUsage { get; set; }

    public string MachineName { get; set; }
    public string EnvironmentName { get; set; }
    public string EnvironmentUserName { get; set; }

    public IApplicationContext Context { get; set; }

    public ExceptionModel Exception { get; set; }
}

public class HttpRequestLog
{
    public HttpRequestLog() { }

    public HttpRequestLog(HttpRequest request, string requestBody)
    {
        DisplayUrl = request.GetDisplayUrl();
        Protocol = request.Protocol;
        Method = request.Method;
        Scheme = request.Scheme;
        PathBase = request.PathBase;
        Path = request.Path;
        QueryString = request.QueryString.Value ?? string.Empty;
        ContentType = request.ContentType ?? string.Empty;
        ContentLength = request.ContentLength;
        Headers = request.Headers.ToStringDictionary();
        Body = requestBody;
    }

    public string DisplayUrl { get; set; }
    public string Protocol { get; set; }
    public string Method { get; set; }
    public string Scheme { get; set; }
    public string PathBase { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string ContentType { get; set; }
    public long? ContentLength { get; set; }
    public string Body { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}

public class HttpResponseLog
{
    public HttpResponseLog() { }

    public HttpResponseLog(HttpResponse response, string responseBody)
    {
        ContentType = response.ContentType;
        ContentLength = response.ContentLength;
        Body = responseBody;
        Headers = response.Headers.ToStringDictionary();
    }

    public string ContentType { get; set; }
    public long? ContentLength { get; set; }
    public string Body { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}

public class ExceptionModel
{
    public ExceptionModel() { }

    public ExceptionModel(Exception exception)
    {
        HelpLink = exception.HelpLink;
        HResult = exception.HResult;
        Message = exception.Message;
        Source = exception.Source;
        StackTrace = exception.StackTrace;
    }

    public string HelpLink { get; set; }
    public int HResult { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string StackTrace { get; set; }
}

internal static class HeaderDictionaryExtensions
{
    public static Dictionary<string, string> ToStringDictionary(this IHeaderDictionary dictionary)
    {
        var result = new Dictionary<string, string>();

        foreach (var (key, value) in dictionary)
        {
            result.Add(key, value.ToString());
        }

        return result;
    }
}