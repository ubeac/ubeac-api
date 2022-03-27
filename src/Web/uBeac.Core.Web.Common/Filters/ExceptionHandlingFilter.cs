using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ExceptionHandlingFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            var result = new Result(ex);
            context.Result = new ObjectResult(result);
        }
    }
}