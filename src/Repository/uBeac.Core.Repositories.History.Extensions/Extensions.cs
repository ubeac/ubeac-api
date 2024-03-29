﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Repositories;
using uBeac.Repositories.History;

namespace Microsoft.Extensions.DependencyInjection;

public static class HistoryExtensions
{
    public static IHistoryBuilder AddHistory<THistoryRepository>(this IServiceCollection services, IHistoryDefaults defaults)
        where THistoryRepository : class, IHistoryRepository
    {
        services.AddSingleton(defaults);
        services.TryAddScoped<IHistoryManager, HistoryManager>();
        services.TryAddScoped<THistoryRepository>();

        return new HistoryBuilder(services, typeof(THistoryRepository));
    }

    public static IHistoryBuilder AddHistory<THistoryRepository>(this IServiceCollection services)
        where THistoryRepository : class, IHistoryRepository
        => services.AddHistory<THistoryRepository>(new HistoryDefaults());
}