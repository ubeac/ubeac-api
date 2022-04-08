using uBeac.Web;

namespace Microsoft.AspNetCore.Builder;

public static class Extensions
{
    public static IApplicationBuilder UseApiLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpLoggingMiddleware>();
        return app;
    }
}