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

    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext applicationContext)
    {
        MongoDatabase = mongoDbContext.Database;
        Collection = mongoDbContext.Database.GetCollection<TEntity>(GetCollectionName());
        BsonCollection = mongoDbContext.Database.GetCollection<BsonDocument>(GetCollectionName());
        MongoDbContext = mongoDbContext;
        ApplicationContext = applicationContext;
    }

    protected virtual string GetCollectionName()
    {
        return typeof(TEntity).Name;
    }

    protected virtual async Task AddToHistory(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var context = ApplicationContext;

        if (entity is IAuditEntity<TKey> audit) context = audit.Context;

        await History.Add(entity, actionName, context, cancellationToken);
    }

    public virtual async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idFilter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);

        var entity = await Collection.FindOneAndDeleteAsync(idFilter, null, cancellationToken);

        if (entity != null) await AddToHistory(entity, actionName, cancellationToken);
    }

    public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
    {
        await Delete(id, nameof(Delete), cancellationToken);
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

        // If the entity is extend from IAuditEntity, the audit properties (CreatedAt, CreatedBy, etc.) should be set
        if (entity is IAuditEntity<TKey> audit) SetPropertiesOnCreate(audit, ApplicationContext);

        await Collection.InsertOneAsync(entity, null, cancellationToken);

        await AddToHistory(entity, actionName, cancellationToken);
    }

    public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Create(entity, nameof(Create), cancellationToken);
    }

    public virtual async Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // If the entity is extend from IAuditEntity, the audit properties (LastUpdatedAt, LastUpdatedBy, etc.) should be set
        if (entity is IAuditEntity<TKey> audit) SetPropertiesOnUpdate(audit, ApplicationContext);

        var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

        entity = await Collection.FindOneAndReplaceAsync(idFilter, entity, null, cancellationToken);

        if (entity != null) await AddToHistory(entity, actionName, cancellationToken);
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

    private void SetPropertiesOnCreate(IAuditEntity<TKey> entity, IApplicationContext appContext)
    {
        var now = DateTime.Now;
        var userName = appContext.UserName;
        var ip = appContext.UserIp;

        entity.CreatedAt = now;
        entity.CreatedBy = userName;
        entity.CreatedByIp = ip;
        entity.LastUpdatedAt = now;
        entity.LastUpdatedBy = userName;
        entity.LastUpdatedByIp = ip;
        entity.Context = appContext;
    }

    private void SetPropertiesOnUpdate(IAuditEntity<TKey> entity, IApplicationContext appContext)
    {
        var now = DateTime.Now;
        var userName = appContext.UserName;
        var ip = appContext.UserIp;

        entity.LastUpdatedAt = now;
        entity.LastUpdatedBy = userName;
        entity.LastUpdatedByIp = ip;
        entity.Context = appContext;
    }
}

public class MongoEntityRepository<TEntity, TContext> : MongoEntityRepository<Guid, TEntity, TContext>, IEntityRepository<TEntity>
    where TEntity : IEntity
    where TContext : IMongoDBContext
{
    public MongoEntityRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}