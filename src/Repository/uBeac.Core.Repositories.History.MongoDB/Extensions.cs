using uBeac;
using uBeac.Repositories.History.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IHistoryRepositoryRegistration UsingMongoDb<TKey, THistory, TContext>(this IHistoryRegistration registration)
        where TKey : IEquatable<TKey>
        where THistory : class, IHistoryEntity<TKey>, new()
        where TContext : IMongoDBContext
        => registration.Using<MongoHistoryRepository<TKey, THistory, TContext>>();

    public static IHistoryRepositoryRegistration UsingMongoDb<THistory, TContext>(this IHistoryRegistration registration)
        where THistory : class, IHistoryEntity, new()
        where TContext : IMongoDBContext
        => registration.Using<MongoHistoryRepository<THistory, TContext>>();

    public static IHistoryRepositoryRegistration UsingMongoDb<TContext>(this IHistoryRegistration registration)
        where TContext : IMongoDBContext
        => registration.Using<MongoHistoryRepository<TContext>>();

    public static IHistoryRepositoryRegistration UsingMongoDb(this IHistoryRegistration registration)
        => registration.Using<MongoHistoryRepository<MongoDBContext>>();
}