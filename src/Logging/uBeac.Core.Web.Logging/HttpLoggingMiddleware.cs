using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web.Logging;

internal class HttpLoggingMiddleware<TKey, THttpLog>
    where TKey : IEquatable<TKey>
    where THttpLog : HttpLog<TKey>, new()
{
    protected readonly RequestDelegate Next;
    protected readonly Stopwatch Stopwatch;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        Next = next;
        Stopwatch = new Stopwatch();
    }

    public virtual async Task Invoke(HttpContext context, IHttpLogRepository<TKey, THttpLog> repository, IApplicationContext appContext)
    {
        Stopwatch.Reset();
        Stopwatch.Start();

        var requestBody = await ReadRequestBody(context.Request);
        var responseBody = await ReadResponseBody(context);

        Stopwatch.Stop();

        var logModel = CreateLogModel(context, appContext, requestBody, responseBody);
        await Log(logModel, repository);
    }

    protected async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return requestBody;
    }

    protected async Task<string> ReadResponseBody(HttpContext context)
    {
        var originalResponseStream = context.Response.Body;

        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await Next(context);

        memoryStream.Position = 0;
        using var reader = new StreamReader(memoryStream, encoding: Encoding.UTF8);
        var responseBody = await reader.ReadToEndAsync();
        memoryStream.Position = 0;
        await memoryStream.CopyToAsync(originalResponseStream);
        context.Response.Body = originalResponseStream;

        return responseBody;
    }

    protected THttpLog CreateLogModel(HttpContext context, IApplicationContext appContext, string requestBody, string responseBody)
        => new()
        {
            Request = new HttpRequestLog(context.Request, requestBody),
            Response = new HttpResponseLog(context.Response, responseBody),
            StatusCode = context.Response.StatusCode,
            Duration = Stopwatch.ElapsedMilliseconds,
            Context = appContext,
            Exception = context.Features.Get<IExceptionHandlerFeature>()?.Error
        };

    protected async Task Log(THttpLog log, IHttpLogRepository<TKey, THttpLog> repository)
    {
        await repository.Create(log);
    }
}

internal class HttpLoggingMiddleware<THttpLog> : HttpLoggingMiddleware<Guid, THttpLog>
    where THttpLog : HttpLog, new()
{
    public HttpLoggingMiddleware(RequestDelegate next) : base(next)
    {
    }
}

internal class HttpLoggingMiddleware : HttpLoggingMiddleware<HttpLog>
{
    public HttpLoggingMiddleware(RequestDelegate next) : base(next)
    {
    }
}