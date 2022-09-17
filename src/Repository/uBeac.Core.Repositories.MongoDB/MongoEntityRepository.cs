using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace uBeac.Repositories.MongoDB;

public class MongoEntityRepository<TKey, TEntity, TContext> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
    where TContext : IMongoDBContext
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoCollection<BsonDocument> BsonCollection;
    protected readonly IMongoDatabase MongoDatabase;
    protected readonly TContext MongoDbContext;
    protected readonly IApplicationContext ApplicationContext;

    protected readonly IEntityEventManager<TKey, TEntity> EventManager;

    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext applicationContext, IEntityEventManager<TKey, TEntity> eventManager)
    {
        MongoDatabase = mongoDbContext.Database;
        Collection = mongoDbContext.Database.GetCollection<TEntity>(GetCollectionName());
        BsonCollection = mongoDbContext.Database.GetCollection<BsonDocument>(GetCollectionName());
        MongoDbContext = mongoDbContext;
        ApplicationContext = applicationContext;
        EventManager = eventManager;
    }

    protected virtual string GetCollectionName()
    {
        return typeof(TEntity).Name;
    }

    public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Delete(entity, nameof(Delete), cancellationToken);
    }

    public async Task Delete(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await EventManager.OnDeleting(entity, actionName, cancellationToken);

        var idFilter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
        await Collection.DeleteOneAsync(idFilter, null, cancellationToken);

        await EventManager.OnDeleted(entity, actionName, cancellationToken);
    }

    public virtual async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
    {
        var entity = await GetById(id, cancellationToken);
        await Delete(entity, actionName, cancellationToken);
    }

    public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
    {
        await Delete(id, nameof(Delete), cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filter = Builders<TEntity>.Filter.Empty;

        return (await Collection.FindAsync(filter, null, cancellationToken)).ToEnumerable();
    }

    public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
        var findResult = await Collection.FindAsync(idFilter, null, cancellationToken);

        return await findResult.SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
        var findResult = await Collection.FindAsync(idsFilter, null, cancellationToken);

        return await findResult.ToListAsync(cancellationToken);
    }

    public virtual async Task Create(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await EventManager.OnCreating(entity, actionName, cancellationToken);

        await Collection.InsertOneAsync(entity, null, cancellationToken);

        await EventManager.OnCreated(entity, actionName, cancellationToken);
    }

    public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Create(entity, nameof(Create), cancellationToken);
    }

    public virtual async Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await EventManager.OnUpdating(entity, actionName, cancellationToken);

        var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        await Collection.FindOneAndReplaceAsync(idFilter, entity, null, cancellationToken);

        await EventManager.OnUpdated(entity, actionName, cancellationToken);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Update(entity, nameof(Update), cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var findResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await findResult.ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> AsQueryable() => Collection.AsQueryable();
}

public class MongoEntityRepository<TEntity, TContext> : MongoEntityRepository<Guid, TEntity, TContext>, IEntityRepository<TEntity>
    where TEntity : IEntity
    where TContext : IMongoDBContext
{
    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext applicationContext, IEntityEventManager<TEntity> eventManager) : base(mongoDbContext, applicationContext, eventManager)
    {
    }
}