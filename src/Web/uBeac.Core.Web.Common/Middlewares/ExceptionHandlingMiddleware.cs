using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace uBeac.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiResult = new ApiResult(exception)
            {
                Code = StatusCodes.Status500InternalServerError,
                TraceId = context.TraceIdentifier
            };

            var serializedResult = JsonConvert.SerializeObject(apiResult);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiResult.Code;
            return context.Response.WriteAsync(serializedResult);
        }

    }
}
