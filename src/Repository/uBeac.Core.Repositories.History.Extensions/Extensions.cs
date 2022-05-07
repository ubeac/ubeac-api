using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Repositories;
using uBeac.Repositories.History;

namespace Microsoft.Extensions.DependencyInjection;

public static class HistoryExtensions
{
    public static HistoryBuilder AddHistory<THistoryRepository>(this IServiceCollection services) where THistoryRepository : class, IHistoryRepository
    {
        services.TryAddScoped<HistoryFactory>();
        services.TryAddScoped<THistoryRepository>();
        return new HistoryBuilder(services, typeof(THistoryRepository));
    }
}