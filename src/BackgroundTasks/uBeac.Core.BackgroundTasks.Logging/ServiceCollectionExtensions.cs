using uBeac.BackgroundTasks.Logging;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundTaskLogging<TEntity, TRepository, TService>(this IServiceCollection services)
        where TEntity : BackgroundTaskLog
        where TRepository : class, IBackgroundTaskLogRepository<TEntity>
        where TService : class, IBackgroundTaskLogService<TEntity>
    {
        services.AddScoped<IBackgroundTaskLogRepository<TEntity>, TRepository>();
        services.AddScoped<IBackgroundTaskLogService<TEntity>, TService>();

        return services;
    }

    public static IServiceCollection AddBackgroundTaskLogging<TRepository, TService>(this IServiceCollection services)
        where TRepository : class, IBackgroundTaskLogRepository
        where TService : class, IBackgroundTaskLogService
    {
        services.AddScoped<IBackgroundTaskLogRepository, TRepository>();
        services.AddScoped<IBackgroundTaskLogService, TService>();

        return services;
    }

    public static IServiceCollection AddBackgroundTaskLogging<TRepository>(this IServiceCollection services)
        where TRepository : class, IBackgroundTaskLogRepository
    {
        services.AddScoped<IBackgroundTaskLogRepository, TRepository>();
        services.AddScoped<IBackgroundTaskLogService, BackgroundTaskLogService>();

        return services;
    }
}
