using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web;

public class ResultFilter : IAsyncActionFilter
{
    protected readonly IDebugger Debugger;
    protected readonly IApplicationContext AppContext;

    public ResultFilter(IDebugger debugger, IApplicationContext appContext)
    {
        Debugger = debugger;
        AppContext = appContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var startTime = DateTime.Now;

        var actionContext = await next();

        if (!IsIResult(context))
            return;

        if (actionContext.Exception != null)
        {
            var exceptionResult = new Result(actionContext.Exception)
            {
                TraceId = AppContext.TraceId,
                SessionId = AppContext.SessionId,
                Debug = Debugger.GetValues(),
                Duration = (DateTime.Now - startTime).TotalMilliseconds,
                Code = StatusCodes.Status500InternalServerError,
            };

            actionContext.ExceptionHandled = true;
            actionContext.Result = new ObjectResult(exceptionResult);

            actionContext.HttpContext.Response.StatusCode = exceptionResult.Code;

            return;
        }

        if (actionContext.Result is null || typeof(ObjectResult) != actionContext.Result.GetType()) return;

        var objectResult = ((ObjectResult)actionContext.Result).Value;

        if (objectResult is IResult result)
        {
            result.TraceId = AppContext.TraceId;
            result.SessionId = AppContext.SessionId;
            result.Debug = Debugger.GetValues();
            result.Duration = (DateTime.Now - startTime).TotalMilliseconds;
            context.HttpContext.Response.StatusCode = result.Code;
        }

    }

    private bool IsIResult(ActionExecutingContext context)
    {
        if (context.ActionDescriptor is null)
            return false;

        var actionDescriptorType = context.ActionDescriptor.GetType();
        if (typeof(ControllerActionDescriptor) != actionDescriptorType)
            return false;

        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        var returnType = controllerActionDescriptor.MethodInfo.ReturnType;
        if (returnType == null)
            return false;

        // if return type id IResult
        if (returnType.GetInterfaces().Any(i => i == typeof(IResult)))
            return true;

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var genericArguments = returnType.GetGenericArguments();
            if (genericArguments.Length == 1 && genericArguments[0].GetInterfaces().Any(i => i == typeof(IResult)))
                return true;
        }

        return false;
    }

}