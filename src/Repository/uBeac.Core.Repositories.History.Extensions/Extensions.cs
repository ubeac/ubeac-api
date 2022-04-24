using uBeac;
using uBeac.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class HistoryExtensions
{
    public static IHistoryRegistration AddHistory(this IServiceCollection services)
    {
        return new HistoryRegistration(services);
    }

    public static IHistoryRepositoryRegistration Using<TRepository>(this IHistoryRegistration registration)
        where TRepository : class, IHistoryRepository
    {
        registration.Services.AddSingleton<TRepository>();
        return new HistoryRepositoryRegistration<TRepository>(registration);
    }

    public static IHistoryRepositoryRegistration For<TData>(this IHistoryRepositoryRegistration registration)
        where TData : class
    {
        var repository = registration.HistoryRegistration.Services.BuildServiceProvider()
            .GetRequiredService(registration.RepositoryType) as IHistoryRepository;

        History.AddRepository(typeof(TData), repository);

        return registration;
    }

    public static IServiceCollection ForDefault(this IHistoryRepositoryRegistration registration)
        => registration.For<object>().HistoryRegistration.Services;
}