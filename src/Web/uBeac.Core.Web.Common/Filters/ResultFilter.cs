using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ResultFilter : IAsyncActionFilter
{
    protected readonly IDebugger Debugger;
    protected readonly IApplicationContext AppContext;

    public ResultFilter(IDebugger debugger, IApplicationContext appContext)
    {
        Debugger = debugger;
        AppContext = appContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var startDate = DateTime.Now;

        var actionContext = await next();

        if (actionContext.Result is null || typeof(ObjectResult) != actionContext.Result.GetType()) return;

        var objectResult = ((ObjectResult)actionContext.Result).Value;

        if (objectResult is IResult result)
        {
            result.TraceId = AppContext.TraceId;
            result.SessionId = AppContext.SessionId;
            result.Debug = Debugger.GetValues();
            result.Duration = (DateTime.Now - startDate).TotalMilliseconds;
            context.HttpContext.Response.StatusCode = result.Code;
        }

    }
}