using MongoDB.Bson;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public class MongoHistoryRepository<TContext> : IHistoryRepository
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

    protected virtual async Task Insert<TData>(HistoryEntity<TData> history, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var dataType = history.Data.GetType();

        var collectionName = GetCollectionName(dataType);
        var collection = MongoDatabase.GetCollection<BsonDocument>(collectionName);

        var bsonDocument = history.ToBsonDocument();
        await collection.InsertOneAsync(bsonDocument, new InsertOneOptions(), cancellationToken);
    }

    public virtual async Task Add<TData>(TData data, string dataId = null, string actionName = "None", IApplicationContext context = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var history = new HistoryEntity
        {
            Data = data,
            DataId = dataId,
            ActionName = actionName,
            Context = context,
            CreatedAt = DateTime.Now
        };

        await Insert(history, cancellationToken);
    }

    public async Task<IEnumerable<HistoryEntity<TData>>> GetAll<TData>(string dataId, CancellationToken cancellationToken = default)
    {
        var collectionName = GetCollectionName(typeof(TData));
        var collection = MongoDatabase.GetCollection<HistoryEntity<TData>>(collectionName);

        var dataIdFilter = Builders<HistoryEntity<TData>>.Filter.Eq(_ => _.DataId, dataId);
        var findResult = await collection.FindAsync(dataIdFilter, null, cancellationToken);
        return await findResult.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<HistoryEntity>> GetAll(Type dataType, string dataId, CancellationToken cancellationToken = default)
    {
        var collectionName = GetCollectionName(dataType);
        var collection = MongoDatabase.GetCollection<HistoryEntity>(collectionName);

        var dataIdFilter = Builders<HistoryEntity>.Filter.Eq(_ => _.DataId, dataId);
        var findResult = await collection.FindAsync(dataIdFilter, null, cancellationToken);
        return await findResult.ToListAsync(cancellationToken);
    }
}