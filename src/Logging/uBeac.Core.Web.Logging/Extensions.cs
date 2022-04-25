using Microsoft.AspNetCore.Builder;

namespace uBeac.Web.Logging;

public static class Extensions
{
    public static IApplicationBuilder UseHttpLoggingMiddleware<TKey, THttpLog>(this IApplicationBuilder app)
        where TKey : IEquatable<TKey>
        where THttpLog : HttpLog<TKey>, new()
    {
        app.UseMiddleware<HttpLoggingMiddleware<TKey, THttpLog>>();
        return app;
    }

    public static IApplicationBuilder UseHttpLoggingMiddleware<THttpLog>(this IApplicationBuilder app)
        where THttpLog : HttpLog, new()
    {
        app.UseMiddleware<HttpLoggingMiddleware<THttpLog>>();
        return app;
    }

    public static IApplicationBuilder UseHttpLoggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpLoggingMiddleware>();
        return app;
    }
}