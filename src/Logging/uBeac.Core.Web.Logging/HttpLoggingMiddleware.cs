using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace uBeac.Web.Logging;

internal sealed class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IHttpLogRepository repository, IApplicationContext appContext, IDebugger debugger)
    {
        try
        {
            var stopwatch = Stopwatch.StartNew();
            Exception exception = null;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (context.LogIgnored() is false)
                {
                    var requestBody = JsonConvert.SerializeObject(context.GetLogRequestBody() ?? new {});
                    var responseBody = JsonConvert.SerializeObject(context.GetLogResponseBody() ?? new {});

                    stopwatch.Stop();

                    var model = context.CreateLogModel(appContext, requestBody, responseBody, stopwatch.ElapsedMilliseconds, exception != null ? 500 : null, exception);
                    await Log(model, repository);
                }
            }
        }
        catch (Exception ex)
        {
            debugger.Add(ex.Message);
        }
    }

    private static async Task Log(HttpLog log, IHttpLogRepository repository) => await repository.Create(log);
}