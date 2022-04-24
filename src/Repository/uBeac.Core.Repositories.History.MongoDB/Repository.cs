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

    public MongoHistoryRepository(TContext mongoDbContext)
    {
        MongoDatabase = mongoDbContext.Database;
        MongoDbContext = mongoDbContext;
    }

    protected virtual string GetCollectionName(Type dataType) => $"{dataType.Name}_History";

    protected virtual async Task Insert(THistory history, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (history.Data == null) throw new NullReferenceException();

        var dataType = history.Data.GetType();

        var collectionName = GetCollectionName(dataType);
        var collection = MongoDatabase.GetCollection<BsonDocument>(collectionName);

        var bsonDocument = history.ToBsonDocument();
        await collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);
    }

    public async Task Add(object data, string actionName = "None", IApplicationContext? context = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var history = new THistory
        {
            Data = data,
            ActionName = actionName,
            Context = context,
            CreatedAt = DateTime.Now
        };

        await Insert(history, cancellationToken);
    }
}

public class MongoHistoryRepository<THistory, TContext> : MongoHistoryRepository<Guid, THistory, TContext>
    where THistory : class, IHistoryEntity, new()
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoHistoryRepository<TContext> : MongoHistoryRepository<HistoryEntity, TContext>
    where TContext : IMongoDBContext
{
    public MongoHistoryRepository(TContext mongoDbContext) : base(mongoDbContext)
    {
    }
}