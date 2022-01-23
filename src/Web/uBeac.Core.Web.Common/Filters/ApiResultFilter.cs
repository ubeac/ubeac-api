using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace uBeac.Web.Filters
{
    public class ApiResultFilter : IActionFilter
    {
        private Stopwatch stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch?.Stop();

            if (context.Result != null && typeof(ObjectResult) == context.Result.GetType())
            {
                var objectResult = (ObjectResult)context.Result;
                if (typeof(IApiResult).IsAssignableFrom(objectResult.Value.GetType()))
                {
                    var result = (IApiResult)objectResult.Value;
                    result.Duration = stopwatch.Elapsed.TotalMilliseconds;
                    result.TraceId = context.HttpContext.TraceIdentifier;
                    context.HttpContext.Response.StatusCode = result.Code;
                }
            }
        }
    }
}
