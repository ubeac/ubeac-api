using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Repositories;

public static class Extensions
{
    public static IServiceCollection AddHistory<TData, THistory>(this IServiceCollection services)
        where TData : class
        where THistory : class, IHistoryRepository<TData>
    {
        services.AddScoped<IHistoryRepository<TData>, THistory>();
        return services;
    }
}