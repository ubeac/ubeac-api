using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace uBeac.Repositories.History.MongoDB;

public class MongoDBHistoryRepository<TKey, THistory> : IHistoryRepository
    where TKey : IEquatable<TKey>
    where THistory : class, IHistoryEntity<TKey>, new()
{
    protected readonly IMongoDatabase Database;
    protected readonly MongoDBSettings _mongoDbSettings;
    private readonly IApplicationContext _applicationContext;

    public MongoDBHistoryRepository(IOptions<MongoDBSettings> mongoDbOptions, IApplicationContext applicationContext)
    {
        _mongoDbSettings = mongoDbOptions.Value;
        _applicationContext = applicationContext;

        var mongoUrl = new MongoUrl(_mongoDbSettings.ConnectionString);
        var client = new MongoClient(mongoUrl);
        Database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    protected virtual string GetCollectionName(Type dataType) => dataType.Name + _mongoDbSettings.CollectionSuffix;

    protected virtual async Task Insert(THistory history, CancellationToken cancellationToken = default)
    {
        var dataType = history.Data.GetType();

        var collectionName = GetCollectionName(dataType);
        var collection = Database.GetCollection<BsonDocument>(collectionName);

        var bsonDocument = history.ToBsonDocument();
        await collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);
    }

    public async Task Add<T>(T data, string actionName, CancellationToken cancellationToken = default)
    {
        var history = new THistory
        {
            Data = data,
            ActionName = actionName,
            Context = _applicationContext,
            CreatedAt = DateTime.Now
        };

        await Insert(history, cancellationToken);
    }
}

public class MongoDBHistoryRepository<THistory> : MongoDBHistoryRepository<Guid, THistory>
    where THistory : class, IHistoryEntity, new()
{
    public MongoDBHistoryRepository(IOptions<MongoDBSettings> mongoDbOptions, IApplicationContext applicationContext) : base(mongoDbOptions, applicationContext)
    {
    }
}

public class MongoDBHistoryRepository : MongoDBHistoryRepository<HistoryEntity>
{
    public MongoDBHistoryRepository(IOptions<MongoDBSettings> mongoDbOptions, IApplicationContext applicationContext) : base(mongoDbOptions, applicationContext)
    {
    }
}