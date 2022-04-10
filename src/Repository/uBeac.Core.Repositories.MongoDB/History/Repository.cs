using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories;

public class MongoHistoryRepository<TKey, THistory, TData, TContext> : IHistoryRepository<TData>
    where TKey : IEquatable<TKey>
    where THistory : IHistory<TKey>
    where TData : class
    where TContext : IMongoDBContext
{
    protected readonly IMongoDatabase MongoDatabase;
    protected readonly TContext MongoDbContext;
    protected readonly IApplicationContext AppContext;
    protected readonly IMongoCollection<THistory> Collection;

    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext)
    {
        MongoDatabase = mongoDbContext.Database;
        MongoDbContext = mongoDbContext;
        Collection = MongoDatabase.GetCollection<THistory>(GetCollectionName());
        AppContext = appContext;
    }

    protected virtual string GetCollectionName() => $"{typeof(TData).Name}.History";

    protected virtual async Task Insert(THistory history, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Collection.InsertOneAsync(history, null, cancellationToken);
    }

    public async Task AddToHistory(TData data, string actionName = "None", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var history = Activator.CreateInstance<THistory>();
        history.Data = data;
        history.Context = AppContext;
        history.CreatedAt = DateTime.Now;

        await Insert(history, cancellationToken);
    }
}

public class MongoHistoryRepository<THistory, TData, TContext> : MongoHistoryRepository<Guid, THistory, TData, TContext>
    where THistory : IHistory
    where TData : class
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}

public class MongoHistoryRepository<TData, TContext> : MongoHistoryRepository<History, TData, TContext>
    where TData : class
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}