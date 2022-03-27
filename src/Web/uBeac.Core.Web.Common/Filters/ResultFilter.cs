using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace uBeac.Web;

public class ResultFilter : IActionFilter
{
    private Stopwatch _stopwatch;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        if (context.Result is null || typeof(ObjectResult) != context.Result.GetType()) return;

        var objectResult = ((ObjectResult)context.Result).Value;
        if (objectResult is IResult result)
        {
            result.Duration = _stopwatch.Elapsed.TotalMilliseconds;
            result.TraceId = context.HttpContext.TraceIdentifier;
            context.HttpContext.Response.StatusCode = result.Code;
        }
    }
}