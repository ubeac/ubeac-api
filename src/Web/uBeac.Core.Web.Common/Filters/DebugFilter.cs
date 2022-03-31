using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class DebugFilter : IAsyncActionFilter
{
    protected readonly IDebugger Debugger;

    public DebugFilter(IDebugger debugger)
    {
        Debugger = debugger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();

        if (context.IsIResult())
        {
            var result = context.GetIResult();
            result.Debug = Debugger.GetValues();
        }
    }
}