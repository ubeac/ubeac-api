using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Context;

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
        var request = context.Request;

        // Don't log if the request was not send to the api endpoints
        if (request.Path.Value?.ToUpper().StartsWith("/API") == false)
        {
            await _next(context);
            return;
        }

        await using var originalRequestBody = request.Body;
        await using var requestBodyStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(requestBodyStream);

        requestBodyStream.Seek(0, SeekOrigin.Begin);
        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        context.Request.Body = requestBodyStream;

        await using var originalResponseBody = context.Response.Body;
        await using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        context.Request.Body = originalRequestBody;

        var response = context.Response;
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalResponseBody);

        _stopwatch.Stop();

        var log = new HttpLog
        {
            DisplayUrl = request.GetDisplayUrl(),
            Protocol = request.Protocol,
            Method = request.Method,
            Scheme = request.Scheme,
            PathBase = request.PathBase,
            Path = request.Path,
            QueryString = request.QueryString.Value,
            Duration = _stopwatch.ElapsedMilliseconds,
            Request =
            {
                ContentType = request.ContentType,
                ContentLength = request.ContentLength,
                Headers = request.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value)),
                Body = requestBody
            },
            Response =
            {
                ContentType = response.ContentType,
                ContentLength = response.ContentLength,
                Body = responseBody,
                Headers = response.Headers.Select(_ => new KeyValuePair<string, object>(_.Key, _.Value)),
                StatusCode = response.StatusCode
            }
        };

        WriteLog(log);
    }

    private void WriteLog(HttpLog log)
    {
        var logLevel = log.Response.StatusCode switch
        {
            < 500 and >= 400 => LogLevel.Warning,
            >= 500 => LogLevel.Error,
            _ => LogLevel.Information
        };

        using (LogContext.PushProperty("HttpLog", log, true))
        {
            _logger.Log(logLevel, $"Request finished {log.DisplayUrl} -- {log.Response.StatusCode} -- {log.Duration}ms");
        }
    }
}