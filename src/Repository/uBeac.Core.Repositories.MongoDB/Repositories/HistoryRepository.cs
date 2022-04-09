using MongoDB.Bson;
using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB;

public class MongoEntityHistoryRepository<TKey, TEntity, TContext> : IEntityHistoryRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
    where TContext : IMongoDBContext
{
    protected readonly bool Enabled;

    protected readonly IMongoCollection<EntityHistory<TKey, TEntity>> Collection;
    protected readonly IMongoCollection<BsonDocument> BsonCollection;
    protected readonly IMongoDatabase MongoDatabase;

    public MongoEntityHistoryRepository(TContext mongoDbContext)
    {
        Enabled = mongoDbContext.HistoryEnabled;
        if (!Enabled) return;

        MongoDatabase = mongoDbContext.HistoryDatabase;
        Collection = mongoDbContext.HistoryDatabase.GetCollection<EntityHistory<TKey, TEntity>>(GetCollectionName());
        BsonCollection = mongoDbContext.HistoryDatabase.GetCollection<BsonDocument>(GetCollectionName());
    }

    protected virtual string GetCollectionName()
    {
        return typeof(TEntity).Name + "_History";
    }

    public async Task AddToHistory(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (!Enabled) return;

        var idFilter = Builders<EntityHistory<TKey, TEntity>>.Filter.Eq(x => x.Id, entity.Id);
        var cursor = await Collection.FindAsync(idFilter, null, cancellationToken);
        var entityHistory = await cursor.SingleOrDefaultAsync(cancellationToken);

        if (entityHistory != null)
        {
            entityHistory.History.Add(entity);
            await Collection.FindOneAndReplaceAsync(idFilter, entityHistory, null, cancellationToken);
        }
        else
        {
            entityHistory = new EntityHistory<TKey, TEntity>
            {
                Id = entity.Id
            };
            entityHistory.History.Add(entity);
            await Collection.InsertOneAsync(entityHistory, null, cancellationToken);
        }
    }
}

public class MongoEntityHistoryRepository<TEntity, TContext> : MongoEntityHistoryRepository<Guid, TEntity, TContext>, IEntityHistoryRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : IMongoDBContext
{
    public MongoEntityHistoryRepository(TContext mongoDbContext) : base(mongoDbContext)
    {
    }
}