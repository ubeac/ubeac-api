using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace uBeac.Web;


public class ModelStateValidationValidationFilter : IAsyncActionFilter
{
    protected readonly IDebugger _debugger;

    public ModelStateValidationValidationFilter(IDebugger debugger)
    {
        _debugger = debugger;
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
                Code = (int)HttpStatusCode.BadRequest,
            };

            foreach (var key in context.ModelState.Keys)
                foreach (var error in context.ModelState[key].Errors)
                    apiResult.Errors.Add(new Error { Code = key, Description = key + " - " + error.ErrorMessage });

            context.Result = new ObjectResult(apiResult);

        }
    }
}