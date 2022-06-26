using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.FileManagement.MongoDB;

public class MongoFileRepository<TKey, TEntity, TContext> : MongoEntityRepository<TKey, TEntity, TContext>, IFileRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
    where TContext : IMongoDBContext
{
    public MongoFileRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}

public class MongoFileRepository<TEntity, TContext> : MongoFileRepository<Guid, TEntity, TContext>, IFileRepository<TEntity>
    where TEntity : IFileEntity
    where TContext : IMongoDBContext
{
    public MongoFileRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}