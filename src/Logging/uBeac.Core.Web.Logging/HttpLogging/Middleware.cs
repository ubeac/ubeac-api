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
        // Don't log if the request was not send to the api endpoints
        if (context.Request.Path.Value?.ToUpper().StartsWith("/API") == false)
        {
            await _next(context);
            return;
        }

        var requestBody = await ReadRequestBody(context.Request);
        var responseBody = await ReadResponseBody(context, _next);

        _stopwatch.Stop();

        var log = CreateLogModel(context, requestBody, responseBody);

        WriteLog(log);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return requestBody;
    }

    private async Task<string> ReadResponseBody(HttpContext context, RequestDelegate next)
    {
        var originalResponseStream = context.Response.Body;

        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        memoryStream.Position = 0;
        using var reader = new StreamReader(memoryStream, encoding: Encoding.UTF8);
        var responseBody = await reader.ReadToEndAsync();
        memoryStream.Position = 0;
        await memoryStream.CopyToAsync(originalResponseStream);
        context.Response.Body = originalResponseStream;

        return responseBody;
    }

    private HttpLog CreateLogModel(HttpContext context, string requestBody, string responseBody)
        => new()
        {
            Duration = _stopwatch.ElapsedMilliseconds,
            Error = context.Features.Get<IExceptionHandlerFeature>()?.Error,
            Request =
            {
                DisplayUrl = context.Request.GetDisplayUrl(),
                Protocol = context.Request.Protocol,
                Method = context.Request.Method,
                Scheme = context.Request.Scheme,
                PathBase = context.Request.PathBase,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.Value ?? string.Empty,
                ContentType = context.Request.ContentType ?? string.Empty,
                ContentLength = context.Request.ContentLength,
                Headers = context.Request.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value)),
                Body = requestBody
            },
            Response =
            {
                ContentType = context.Response.ContentType,
                ContentLength = context.Response.ContentLength,
                Body = responseBody,
                Headers = context.Response.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value)),
                StatusCode = context.Response.StatusCode
            }
        };

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