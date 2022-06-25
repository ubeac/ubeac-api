using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public class MongoEntityHistoryRepository<TKey, TEntity, TContext> : MongoEntityRepository<TKey, TEntity, TContext>, IEntityHistoryRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
    where TContext : IMongoDBContext
{
    public MongoEntityHistoryRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}

public class MongoEntityHistoryRepository<TEntity, TContext> : MongoEntityHistoryRepository<Guid, TEntity, TContext>, IEntityHistoryRepository<TEntity>
    where TEntity : IEntity
    where TContext : IMongoDBContext
{
    public MongoEntityHistoryRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}