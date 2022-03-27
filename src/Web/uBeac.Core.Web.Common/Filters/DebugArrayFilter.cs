using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class DebugArrayFilter : IActionFilter
{
    private readonly IDebugger _debugger;

    public DebugArrayFilter(IDebugger debugger)
    {
        _debugger = debugger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is null || typeof(ObjectResult) != context.Result.GetType()) return;

        var objectResult = ((ObjectResult)context.Result).Value;
        if (objectResult is IResult result) result.Debug = _debugger.GetValues();
    }
}