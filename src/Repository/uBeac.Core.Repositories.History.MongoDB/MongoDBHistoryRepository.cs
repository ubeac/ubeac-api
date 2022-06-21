using MongoDB.Bson;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public interface IMongoDBHistoryRepository : IHistoryRepository
{
}

public class MongoDBHistoryRepository<TKey, THistory, TContext> : IMongoDBHistoryRepository
    where TKey : IEquatable<TKey>
    where THistory : class, IHistoryEntity<TKey>, new()
    where TContext : IMongoDBContext
{
    protected readonly IApplicationContext ApplicationContext;
    protected readonly TContext MongoDbContext;
    protected readonly IMongoDatabase MongoDatabase;
    protected readonly IHistoryDefaults Defaults;

    public MongoDBHistoryRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryDefaults defaults)
    {
        MongoDbContext = mongoDbContext;
        MongoDatabase = MongoDbContext.Database;
        ApplicationContext = applicationContext;
        Defaults = defaults;
    }

    protected virtual string GetCollectionName(Type dataType) => dataType.Name + Defaults.Suffix;

    protected virtual async Task Insert(THistory history, CancellationToken cancellationToken = default)
    {
        var dataType = history.Data.GetType();

        var collectionName = GetCollectionName(dataType);
        var collection = MongoDatabase.GetCollection<BsonDocument>(collectionName);

        var bsonDocument = history.ToBsonDocument();
        await collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);
    }

    public async Task Add<T>(T data, string actionName, CancellationToken cancellationToken = default)
    {
        var history = new THistory
        {
            Data = data,
            ActionName = actionName,
            Context = ApplicationContext,
            CreatedAt = DateTime.Now
        };

        await Insert(history, cancellationToken);
    }
}

public class MongoDBHistoryRepository<THistory, TContext> : MongoDBHistoryRepository<Guid, THistory, TContext>
    where THistory : class, IHistoryEntity, new()
    where TContext : IMongoDBContext
{
    public MongoDBHistoryRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryDefaults defaults) : base(mongoDbContext, applicationContext, defaults)
    {
    }
}

public class MongoDBHistoryRepository<TContext> : MongoDBHistoryRepository<HistoryEntity, TContext>
    where TContext : IMongoDBContext
{
    public MongoDBHistoryRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryDefaults defaults) : base(mongoDbContext, applicationContext, defaults)
    {
    }
}

public class MongoDBHistoryRepository : MongoDBHistoryRepository<HistoryMongoDBContext>
{
    public MongoDBHistoryRepository(HistoryMongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryDefaults defaults) : base(mongoDbContext, applicationContext, defaults)
    {
    }
}