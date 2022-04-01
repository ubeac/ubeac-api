using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next();
        }
        else
        {
            var result = new Result
            {
                Code = StatusCodes.Status400BadRequest
            };

            foreach (var key in context.ModelState.Keys)
                foreach (var error in context.ModelState[key].Errors)
                    result.Errors.Add(new Error { Code = key, Description = key + "," + error.ErrorMessage });

            context.Result = new ObjectResult(result);
        }
    }
}