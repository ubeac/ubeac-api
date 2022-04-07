using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ExceptionHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            var result = new Result(context.Exception);
            context.Result = new ObjectResult(result);
        }
    }
}