using uBeac.Web;

namespace Microsoft.AspNetCore.Builder;

public static class Extensions
{
    public static IApplicationBuilder UseHttpRequestLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpLoggingMiddleware>();
        return app;
    }
}