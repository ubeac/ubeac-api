using uBeac;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
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

    public static IHistoryRepositoryRegistration UsingMongoDb<TKey, THistory, TContext>(this IHistoryRegistration registration)
        where TKey : IEquatable<TKey>
        where THistory : class, IHistoryEntity<TKey>, new()
        where TContext : IMongoDBContext
        => Using<MongoHistoryRepository<TKey, THistory, TContext>>(registration);

    public static IHistoryRepositoryRegistration UsingMongoDb<THistory, TContext>(this IHistoryRegistration registration)
        where THistory : class, IHistoryEntity, new()
        where TContext : IMongoDBContext
        => Using<MongoHistoryRepository<THistory, TContext>>(registration);

    public static IHistoryRepositoryRegistration UsingMongoDb<TContext>(this IHistoryRegistration registration)
        where TContext : IMongoDBContext
        => Using<MongoHistoryRepository<TContext>>(registration);

    public static IHistoryRepositoryRegistration UsingMongoDb(this IHistoryRegistration registration)
        => Using<MongoHistoryRepository<MongoDBContext>>(registration);

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