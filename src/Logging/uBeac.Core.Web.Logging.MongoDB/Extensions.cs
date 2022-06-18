using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoDbHttpLogging(this IServiceCollection services, MongoDbHttpLogOptions options)
    {
        services.AddSingleton(options);
        services.AddScoped<IHttpLogRepository, MongoDbHttpLogRepository>();
        return services;
    }

    public static IServiceCollection AddMongoDbHttpLogging(this IServiceCollection services, Func<MongoDbHttpLogOptions> options)
    {
        return AddMongoDbHttpLogging(services, options());
    }
}