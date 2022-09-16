using uBeac.Repositories.History;
using uBeac.Repositories.History.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoHistoryExtensions
{
    public static IHistoryBuilder AddMongoHistory<TContext>(this IServiceCollection services)
        where TContext : IMongoDBContext
        => new MongoHistoryBuilder<TContext>(services);
}