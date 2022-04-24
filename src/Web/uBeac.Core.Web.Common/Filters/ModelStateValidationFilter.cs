using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class ModelStateValidationFilter : IAsyncActionFilter
{
    protected readonly IApplicationContext AppContext;
    protected readonly IDebugger Debugger;

    public ModelStateValidationFilter(IDebugger debugger, IApplicationContext appContext)
    {
        AppContext = appContext;
        Debugger = debugger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var startTime = DateTime.Now;

        if (context.ModelState.IsValid)
        {
            await next();
        }
        else
        {
            var apiResult = new Result
            {
                Code = StatusCodes.Status400BadRequest,
                TraceId = AppContext.TraceId,
                SessionId = AppContext.SessionId,
                Debug = Debugger.GetValues(),
                Duration = (DateTime.Now - startTime).TotalMilliseconds,
            };

            foreach (var key in context.ModelState.Keys)
                foreach (var error in context.ModelState[key].Errors)
                    apiResult.Errors.Add(new Error(key, key + " - " + error.ErrorMessage));

            context.Result = new ObjectResult(apiResult);
            context.HttpContext.Response.StatusCode = apiResult.Code;
        }
    }
}