using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace uBeac.Web;

internal class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpLoggingMiddleware> _logger;
    private readonly Stopwatch _stopwatch;

    public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _stopwatch = Stopwatch.StartNew();
    }

    public async Task Invoke(HttpContext context)
    {
        var requestBody = await ReadRequestBody(context.Request);
        var responseBody = await ReadResponseBody(context, _next);

        _stopwatch.Stop();

        var log = CreateLogModel(context, requestBody, responseBody);

        WriteLog(log);
    }

    private static async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return requestBody;
    }

    private static async Task<string> ReadResponseBody(HttpContext context, RequestDelegate next)
    {
        var originalResponseStream = context.Response.Body;

        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await next(context);

        memoryStream.Position = 0;
        using var reader = new StreamReader(memoryStream, encoding: Encoding.UTF8);
        var responseBody = await reader.ReadToEndAsync();
        memoryStream.Position = 0;
        await memoryStream.CopyToAsync(originalResponseStream);
        context.Response.Body = originalResponseStream;

        return responseBody;
    }

    private HttpLog CreateLogModel(HttpContext context, string requestBody, string responseBody)
        => new(_stopwatch.ElapsedMilliseconds, new HttpLog.HttpRequest(context.Request, requestBody),
            new HttpLog.HttpResponse(context.Response, responseBody),
            context.Features.Get<IExceptionHandlerFeature>()?.Error);

    private void WriteLog(HttpLog log)
    {
        var logLevel = log.Response.StatusCode switch
        {
            < 500 and >= 400 => LogLevel.Warning,
            >= 500 => LogLevel.Error,
            _ => LogLevel.Information
        };

        ILogEventEnricher[] enrichers =
        {
            new PropertyEnricher("DurationMilliseconds", log.Duration),
            new PropertyEnricher("StatusCode", log.Response.StatusCode),
            new PropertyEnricher("Error", log.Error),
            new PropertyEnricher("Request", log.Request, true),
            new PropertyEnricher("Response", log.Response, true)
        };

        using (LogContext.Push(enrichers))
        {
            _logger.Log(logLevel, $"Request finished {log.Request.DisplayUrl} -- {log.Response.StatusCode} -- {log.Duration}ms");
        }
    }
}