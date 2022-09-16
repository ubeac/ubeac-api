using Microsoft.Extensions.DependencyInjection;
using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public class MongoHistoryBuilder<TContext> : IHistoryBuilder
    where TContext : IMongoDBContext
{
    public IServiceCollection Services { get; }

    public MongoHistoryBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IHistoryBuilder For<T>() => this.AddRepository<T, MongoEntityRepository<HistoryEntity<T>, TContext>>(Services);
}