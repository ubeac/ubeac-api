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
    protected readonly IApplicationContext AppContext;

    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext appContext)
    {
        MongoDatabase = mongoDbContext.Database;
        Collection = mongoDbContext.Database.GetCollection<TEntity>(GetCollectionName());
        BsonCollection = mongoDbContext.Database.GetCollection<BsonDocument>(GetCollectionName());
        MongoDbContext = mongoDbContext;
        AppContext = appContext;
    }

    protected virtual string GetCollectionName()
    {
        return typeof(TEntity).Name;
    }

    public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idFilter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
        var entity = await Collection.FindOneAndDeleteAsync(idFilter, null, cancellationToken);

        await History.AddToHistory(entity, nameof(Delete), cancellationToken);

        return entity != null;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filter = Builders<TEntity>.Filter.Empty;
        var findResult = await Collection.FindAsync(filter, null, cancellationToken);
        return await findResult.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
        var findResult = await Collection.FindAsync(idFilter, null, cancellationToken);
        return await findResult.SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
        var findResult = await Collection.FindAsync(idsFilter, null, cancellationToken);
        return await findResult.ToListAsync(cancellationToken);
    }

    public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // If the entity is extend from IAuditEntity, the audit properties (CreatedAt, CreatedBy, etc.) should be set
        if (entity is IAuditEntity<TKey> audit) audit.SetPropertiesOnCreate(AppContext);

        await Collection.InsertOneAsync(entity, null, cancellationToken);

        await History.AddToHistory(entity, nameof(Create), cancellationToken);
    }

    public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // If the entity is extend from IAuditEntity, the audit properties (LastUpdatedAt, LastUpdatedBy, etc.) should be set
        if (entity is IAuditEntity<TKey> audit) audit.SetPropertiesOnUpdate(AppContext);

        var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        entity = await Collection.FindOneAndReplaceAsync(idFilter, entity, null, cancellationToken);

        await History.AddToHistory(entity, nameof(Update), cancellationToken);

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default)
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
    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}