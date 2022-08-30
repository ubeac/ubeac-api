using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace uBeac.Web.Logging;

public class CriticalDataHandlerFilter : IActionFilter
{
    private readonly IDebugger _debugger;
    private readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
        ContractResolver = new LogIgnoreResolver(),
        Formatting = Formatting.Indented,        
    };
    public CriticalDataHandlerFilter(IDebugger debugger)
    {
        _debugger = debugger;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        try
        {
            if (context.Result.GetType() != typeof(EmptyResult))
            {
                var result = ((ObjectResult)context.Result).Value;
                context.HttpContext.Items["LogResponseBody"] = result != null ? JsonConvert.SerializeObject(result, settings) : null;
            }
        }
        catch (Exception ex)
        {
            _debugger.Add("HttpLogging: " + ex.Message);
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var args = context.ActionArguments?.Where(x => x.Value.GetType() != typeof(CancellationToken)).ToList();
        context.HttpContext.Items["LogRequestBody"] = args != null ? JsonConvert.SerializeObject(args, settings) : null;
    }
}

