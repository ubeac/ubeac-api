using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace uBeac.Web.Logging;

public class HttpLogDataHandlingFilter : IActionFilter, IOrderedFilter
{
    private readonly JsonSerializerSettings _serializationSettings = new()
    {
        ContractResolver = new JsonLogResolver(),
        Formatting = Formatting.Indented
    };

    private bool _logIgnored;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var ignoredController = context.Controller.GetType().IsIgnored();
        var ignoredAction = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.IsIgnored();
        _logIgnored = ignoredController || ignoredAction;
        context.HttpContext.SetLogIgnored(_logIgnored);
        if (_logIgnored) return;

        var requestArgs = context.ActionArguments.Where(arg => arg.Value == null || arg.Value.GetType() != typeof(CancellationToken)).ToList();
        var logRequestBody = JsonConvert.SerializeObject(requestArgs, _serializationSettings);
        context.HttpContext.SetLogRequestBody(logRequestBody);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (_logIgnored) return;

        if (context.Result == null || context.Result.GetType() == typeof(EmptyResult))
        {
            var emptyResult = JsonConvert.SerializeObject(new { }, _serializationSettings);
            context.HttpContext.SetLogResponseBody(emptyResult);
            return;
        }

        var resultValue = ((ObjectResult)context.Result).Value;
        var logResponseBody = resultValue != null ? JsonConvert.SerializeObject(resultValue, _serializationSettings) : null;
        context.HttpContext.SetLogResponseBody(logResponseBody);
    }

    public int Order => int.MaxValue;
}

internal class JsonLogResolver : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var result = base.CreateProperty(member, memberSerialization);

        // Ignore value
        result.Ignored = member.IsIgnored();
        if (result.Ignored) return result;

        // Replace value
        if (member.HasReplaceValue())
        {
            var replaceValue = member.GetReplaceValue();
            result.ValueProvider = new ReplaceValueProvider(replaceValue);
        }
        
        return result;
    }
}

internal class ReplaceValueProvider : IValueProvider
{
    private readonly object _replaceValue;

    public ReplaceValueProvider(object replaceValue)
    {
        _replaceValue = replaceValue;
    }

    public void SetValue(object target, object value) { }
    public object GetValue(object target) => _replaceValue;
}