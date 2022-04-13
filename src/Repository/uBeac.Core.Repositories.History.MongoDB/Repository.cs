using MongoDB.Bson;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public class MongoHistoryRepository<TKey, THistory, TContext> : IHistoryRepository
    where TKey : IEquatable<TKey>
    where THistory : class, IHistoryEntity<TKey>, new()
    where TContext : IMongoDBContext
{
    protected readonly IMongoDatabase MongoDatabase;
    protected readonly TContext MongoDbContext;
    protected readonly IApplicationContext AppContext;

    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext)
    {
        MongoDatabase = mongoDbContext.Database;
        MongoDbContext = mongoDbContext;
        AppContext = appContext;
    }

    protected virtual string GetCollectionName(Type dataType) => $"{dataType.Name}.History";

    protected virtual async Task Insert(THistory history, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var dataType = history.Data.GetType();

        var collectionName = GetCollectionName(dataType);
        var collection = MongoDatabase.GetCollection<BsonDocument>(collectionName);

        var bsonDocument = history.ToBsonDocument();
        await collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);
    }

    protected virtual THistory CreateInstance(object data, string actionName)
    {
        var history = Activator.CreateInstance<THistory>();
        history.Data = data;
        history.ActionName = actionName;
        history.CreatedAt = DateTime.Now;
        history.Context = AppContext.ToModel();
        return history;
    }

    public async Task AddToHistory(object data, string actionName = "None", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var history = CreateInstance(data, actionName);
        await Insert(history, cancellationToken);
    }

    public async Task AddToHistory<TData>(TData data, string actionName = "None", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var history = CreateInstance(data, actionName);
        await Insert(history, cancellationToken);
    }
}

public class MongoHistoryRepository<THistory, TContext> : MongoHistoryRepository<Guid, THistory, TContext>
    where THistory : class, IHistoryEntity, new()
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}

public class MongoHistoryRepository<TContext> : MongoHistoryRepository<HistoryEntity, TContext>
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}