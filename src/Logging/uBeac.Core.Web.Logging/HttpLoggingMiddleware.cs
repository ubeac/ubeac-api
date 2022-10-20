using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web.Logging;

internal sealed class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IHttpLogRepository repository, IApplicationContext appContext, IDebugger debugger, IHttpLogChanges httpLogChanges)
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
                stopwatch.Stop();

                if (!httpLogChanges.ContainsKey(LogConstants.LOG_IGNORED) || httpLogChanges[LogConstants.LOG_IGNORED] is false)
                {
                    var model = context.CreateLogModel(appContext, httpLogChanges[LogConstants.REQUEST_BODY].ToString(), httpLogChanges[LogConstants.RESPONSE_BODY].ToString(), stopwatch.ElapsedMilliseconds, exception != null ? 500 : null, exception);
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