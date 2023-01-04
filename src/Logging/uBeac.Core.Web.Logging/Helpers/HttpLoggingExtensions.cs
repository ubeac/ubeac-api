using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web.Logging;

internal static class HttpLoggingExtensions
{
    public static HttpLog CreateLogModel(this HttpContext context, IApplicationContext appContext, string requestBody, string responseBody, long duration, int? statusCode = null, Exception exception = null)
    {
        exception ??= context.Features.Get<IExceptionHandlerFeature>()?.Error;

        return new HttpLog
        {
            Request = new HttpRequestLog(context.Request, requestBody),
            Response = new HttpResponseLog(context.Response, responseBody),
            StatusCode = statusCode ?? context.Response.StatusCode,
            Duration = duration,
            Context = appContext,
            Exception = exception == null ? null : new ExceptionModel(exception)
        };
    }

    public static bool IsIgnored(this MemberInfo target) => target.GetCustomAttributes(typeof(LogIgnoreAttribute), true).Any();

    public static bool HasReplaceValue(this MemberInfo target) => target.GetCustomAttributes(typeof(LogReplaceValueAttribute), true).Any();
    public static object GetReplaceValue(this MemberInfo target) => ((LogReplaceValueAttribute)target.GetCustomAttributes(typeof(LogReplaceValueAttribute), true).First()).Value;
}