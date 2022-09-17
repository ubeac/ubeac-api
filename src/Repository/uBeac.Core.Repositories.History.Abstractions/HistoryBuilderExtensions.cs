using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace uBeac.Repositories.History;

public static class HistoryBuilderExtensions
{
    public static IHistoryBuilder AddRepository<TData, TRepository>(this IHistoryBuilder builder, IServiceCollection services)
        where TRepository : class, IEntityRepository<HistoryEntity<TData>>
    {
        services.TryAddScoped<IHistoryManager, HistoryManager>();

        services.AddScoped<IEntityRepository<HistoryEntity<TData>>, TRepository>();
        services.AddScoped<IGeneralEntityEventHandler, HistoryEventHandler>();

        return builder;
    }
}