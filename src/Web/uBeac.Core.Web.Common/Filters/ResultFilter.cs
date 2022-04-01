using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ResultFilter : IAsyncAlwaysRunResultFilter
{
    protected readonly IDebugger Debugger;
    protected readonly IApplicationContext AppContext;

    public ResultFilter(IDebugger debugger, IApplicationContext appContext)
    {
        Debugger = debugger;
        AppContext = appContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is null || typeof(ObjectResult) != context.Result.GetType()) return;

        var objectResult = ((ObjectResult)context.Result).Value;
        if (objectResult is IResult result)
        {
            result.TraceId = AppContext.TraceId;
            result.SessionId = AppContext.SessionId;
            result.Debug = Debugger.GetValues();
            context.HttpContext.Response.StatusCode = result.Code;
            result.Duration = 0; // TODO: Set duration
        }

        await next();
    }
}