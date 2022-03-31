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

        await next();

        if (context.IsIResult())
        {
            var result = context.GetIResult();
            result.TraceId = AppContext.TraceId;
            result.SessionId = AppContext.SessionId;
            result.Duration = (DateTime.Now - startDate).TotalMilliseconds;
        }
    }
}