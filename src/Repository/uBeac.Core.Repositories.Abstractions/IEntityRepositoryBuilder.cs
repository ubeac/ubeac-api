using uBeac;
using uBeac.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public interface IEntityRepositoryBuilder
{
    Type Key { get; }
    Type Entity { get; }
    Type Interface { get; }
    Type Implementation { get; }
    ServiceLifetime Lifetime { get; }
}

public class EntityRepositoryBuilder<TKey, TEntity, TInterface, TImplementation> : IEntityRepositoryBuilder
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
    where TInterface : IEntityRepository<TKey, TEntity>
    where TImplementation : class, TInterface
{
    public Type Key { get; } = typeof(TKey);
    public Type Entity { get; } = typeof(TEntity);
    public Type Interface { get; } = typeof(TInterface);
    public Type Implementation { get; } = typeof(TImplementation);
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
}