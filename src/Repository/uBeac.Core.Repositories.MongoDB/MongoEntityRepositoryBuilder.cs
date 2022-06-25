using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Repositories.MongoDB;

public class MongoEntityRepositoryBuilder<TKey, TEntity, TContext, TInterface, TImplementation> : EntityRepositoryBuilder<TKey, TEntity, TInterface, TImplementation>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
    where TContext : IMongoDBContext
    where TInterface : IEntityRepository<TKey, TEntity>
    where TImplementation : MongoEntityRepository<TKey, TEntity, TContext>, TInterface
{
}