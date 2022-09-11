using uBeac.Web.Logging;
using uBeac.Web.Logging.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddEFHttpLogging<TContext>(this IServiceCollection services)
        where TContext : HttpLogDbContext
    {
        services.AddScoped<IHttpLogRepository, EFHttpLogRepository<TContext>>();        
        return services;
    }
}