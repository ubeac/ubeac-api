using uBeac.Repositories.MongoDB;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoDbHttpLogging<TContext>(this IServiceCollection services, MongoDbHttpLogOptions options)
        where TContext : IMongoDBContext
    {
        services.AddSingleton(options);
        services.AddScoped<IHttpLogRepository, MongoDbHttpLogRepository<TContext>>();
        return services;
    }
}