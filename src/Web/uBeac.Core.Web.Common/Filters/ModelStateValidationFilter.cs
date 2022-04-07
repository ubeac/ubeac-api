using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web;

public class ModelStateValidationFilter : IAsyncActionFilter
{
    protected readonly IDebugger Debugger;

    public ModelStateValidationFilter(IDebugger debugger)
    {
        Debugger = debugger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next();
        }
        else
        {
            var apiResult = new Result
            {
                Code = StatusCodes.Status400BadRequest
            };

            foreach (var key in context.ModelState.Keys)
                foreach (var error in context.ModelState[key].Errors)
                    apiResult.Errors.Add(new Error { Code = key, Description = key + " - " + error.ErrorMessage });

            context.Result = new ObjectResult(apiResult);
        }
    }
}