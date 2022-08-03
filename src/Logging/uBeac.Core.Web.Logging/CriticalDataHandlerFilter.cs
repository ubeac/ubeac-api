using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace uBeac.Web.Logging;

public class CriticalDataHandlerFilter : IActionFilter
{
    private readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
        ContractResolver = new LogIgnoreResolver(),
        Formatting = Formatting.Indented
    };
    public void OnActionExecuted(ActionExecutedContext context)
    {
        try
        {
            var result = ((ObjectResult)context.Result).Value;
            //var responseProps = result.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(LogIgnoreAttribute))).ToList();
            //foreach (var prop in responseProps)
            //{
            //    prop.SetValue(result, default);
            //}
            context.HttpContext.Items["LogResponseBody"] = result != null ? JsonConvert.SerializeObject(result, settings) : null;
        }
        catch (Exception ex)
        {

        }        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var args = context.ActionArguments?.Where(x => x.Value.GetType() != typeof(CancellationToken)).ToList();
        context.HttpContext.Items["LogRequestBody"] = args != null ? JsonConvert.SerializeObject(args, settings) : null;
    }
}

