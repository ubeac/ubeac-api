using uBeac.Repositories.MongoDB;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoDbHttpLogging<TContext>(this IServiceCollection services, string connectionString, MongoDbHttpLogOptions options)
        where TContext : class, IMongoDBContext
    {
        services.AddSingleton<HttpLogCache>();
        services.AddMongo<TContext>(connectionString);
        services.AddSingleton(options);
        services.AddScoped<IHttpLogRepository, MongoDbHttpLogRepository<TContext>>();        
        return services;
    }
}