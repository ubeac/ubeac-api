using uBeac.Repositories.History.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IHistoryRepositoryRegistration UsingMongoDb<TContext>(this IHistoryRegistration registration)
        where TContext : IMongoDBContext
        => registration.Using<MongoHistoryRepository<TContext>>();

    public static IHistoryRepositoryRegistration UsingMongoDb(this IHistoryRegistration registration)
        => registration.Using<MongoHistoryRepository<MongoDBContext>>();
}