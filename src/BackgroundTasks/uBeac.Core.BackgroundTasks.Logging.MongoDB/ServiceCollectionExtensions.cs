using uBeac.BackgroundTasks.Logging.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoBackgroundTaskLogging<TContext>(this IServiceCollection services)
        where TContext : IMongoDBContext
    {
        services.AddBackgroundTaskLogging<MongoBackgroundTaskLogRepository<TContext>>();

        return services;
    }
}
