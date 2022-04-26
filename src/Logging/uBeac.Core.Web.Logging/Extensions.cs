using Microsoft.AspNetCore.Builder;

namespace uBeac.Web.Logging;

public static class Extensions
{
    public static IApplicationBuilder UseHttpLoggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpLoggingMiddleware>();
        return app;
    }
}