using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace uBeac.Web.Logging;

public class CriticalDataHandlerFilter : Attribute, IActionFilter

{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        try
        {
            var actionDescriptor = context.ActionDescriptor;
            if (actionDescriptor != null)
            {
                var requestBody = context.HttpContext.Items["LogRequestBody"] != null ? context.HttpContext.Items["LogRequestBody"] : null;
                var props = requestBody.GetType().GetProperties().Where(prop => IsDefined(prop, typeof(LogIgnoreAttribute))).ToList();

                foreach (var prop in props)
                {
                    prop.SetValue(requestBody, default);
                }
                context.HttpContext.Items["LogRequestBody"] = requestBody;
            }

            var result = ((ObjectResult)context.Result).Value;
            var responseProps = result.GetType().GetProperties().Where(prop => IsDefined(prop, typeof(LogIgnoreAttribute))).ToList();
            foreach (var prop in responseProps)
            {
                prop.SetValue(result, default);
            }
            context.HttpContext.Items["LogResponseBody"] = result;
        }
        catch (Exception ex)
        {

            throw;
        }        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var arg = context.ActionArguments.First().Value;
        context.HttpContext.Items["LogRequestBody"] = arg;

        //try
        //{
        //    var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        //    if (controllerActionDescriptor != null)
        //    {
        //        var requestBody = FormatRequestBody(context.ActionArguments);
        //        context.HttpContext.Items["LogRequestBody"] = requestBody;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    _logger.Error("Error in LogServiceCallFilter", ex);
        //}
        //var arg = context.ActionArguments.First().Value;
        //var argType = arg.GetType();
        //var props = arg.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(LogIgnoreAttribute))).ToList();

        //foreach (var prop in props)
        //{
        //    prop.SetValue(arg, default);
        //}
    }
}

