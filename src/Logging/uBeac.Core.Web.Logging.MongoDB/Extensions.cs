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
}