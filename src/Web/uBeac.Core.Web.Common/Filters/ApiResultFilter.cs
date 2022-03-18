using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace uBeac.Web.Filters
{
    public class ApiResultFilter : IActionFilter
    {
        private Stopwatch _stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            if (context.Result != null && typeof(ObjectResult) == context.Result.GetType())
            {
                var objectResult = (ObjectResult)context.Result;
                if (objectResult.Value is IResult result)
                {
                    result.Duration = _stopwatch.Elapsed.TotalMilliseconds;
                    result.TraceId = context.HttpContext.TraceIdentifier;
                    context.HttpContext.Response.StatusCode = result.Code;
                }
            }
        }
    }
}
