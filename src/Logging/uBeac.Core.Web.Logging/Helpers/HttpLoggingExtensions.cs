using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace uBeac.Web.Logging;

internal static class HttpLoggingExtensions
{
    private const string LOG_REQUEST_BODY = "_LogRequestBody";
    private const string LOG_RESPONSE_BODY = "_LogResponseBody";
    private const string LOG_IGNORED = "_LogIgnored";

    public static void SetLogRequestBody(this HttpContext context, object requestBody) => context.Items[LOG_REQUEST_BODY] = requestBody;
    public static object GetLogRequestBody(this HttpContext context) => context.Items[LOG_REQUEST_BODY];

    public static void SetLogResponseBody(this HttpContext context, object responseBody) => context.Items[LOG_RESPONSE_BODY] = responseBody;
    public static object GetLogResponseBody(this HttpContext context) => context.Items[LOG_RESPONSE_BODY];

    public static void SetLogIgnored(this HttpContext context, bool ignored = true) => context.Items[LOG_IGNORED] = ignored;
    public static bool LogIgnored(this HttpContext context) => context.Items[LOG_IGNORED] != null && (bool)context.Items[LOG_IGNORED];

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