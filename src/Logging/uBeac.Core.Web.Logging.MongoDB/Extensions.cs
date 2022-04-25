using uBeac.Repositories.MongoDB;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoHttpLogging<TKey, THttpLog, TContext>(this IServiceCollection services)
        where TKey : IEquatable<TKey>
        where THttpLog : HttpLog<TKey>, new()
        where TContext : IMongoDBContext
    {
        services.AddScoped<IHttpLogRepository<TKey, THttpLog>, MongoHttpLogRepository<TKey, THttpLog, TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoHttpLogging<THttpLog, TContext>(this IServiceCollection services)
        where THttpLog : HttpLog, new()
        where TContext : IMongoDBContext
    {
        services.AddMongoHttpLogging<Guid, THttpLog, TContext>();
        services.AddScoped<IHttpLogRepository<THttpLog>, MongoHttpLogRepository<THttpLog, TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoHttpLogging<TContext>(this IServiceCollection services)
        where TContext : IMongoDBContext
    {
        services.AddMongoHttpLogging<HttpLog, TContext>();
        services.AddScoped<IHttpLogRepository, MongoHttpLogRepository<TContext>>();
        return services;
    }

    public static IServiceCollection AddMongoHttpLogging(this IServiceCollection services)
    {
        services.AddMongoHttpLogging<MongoDBContext>();
        services.AddScoped<IHttpLogRepository, MongoHttpLogRepository<MongoDBContext>>();
        return services;
    }
}