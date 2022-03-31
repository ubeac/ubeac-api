using Microsoft.AspNetCore.Http;
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
            if (context.IsIResult())
            {
                var result = context.GetIResult();
                result.Code = StatusCodes.Status500InternalServerError;
                result.Errors.Add(new Error(ex));
            }
            else
            {
                throw;
            }
        }
    }
}