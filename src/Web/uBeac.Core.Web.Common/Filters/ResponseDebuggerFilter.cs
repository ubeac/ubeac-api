using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web.Filters;

public class ResponseDebuggerFilter : IActionFilter
{
    private readonly IDebugger _debugger;

    public ResponseDebuggerFilter(IDebugger debugger)
    {
        _debugger = debugger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is null) return;
        if (typeof(ObjectResult) != context.Result.GetType()) return;

        var result = ((ObjectResult)context.Result).Value;
        if (result is ApiResult) ((ApiResult)result).Debug = _debugger.GetValues();
    }
}