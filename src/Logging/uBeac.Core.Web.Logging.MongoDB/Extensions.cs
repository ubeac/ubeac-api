using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMongoDbHttpLogging(this IServiceCollection services, HttpLoggingMongoDbOptions options)
    {
        services.AddSingleton(options);

        services.AddSingleton<IHttpLoggingMongoDbContext, HttpLoggingMongoDbContext>();

        services.AddScoped<IHttpLoggingRepository, HttpLoggingMongoDbRepository>();

        return services;
    }
}