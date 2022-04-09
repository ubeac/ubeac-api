namespace uBeac;

public class EntityHistory<TKey, TEntity> : Entity<TKey>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    public virtual List<TEntity> History { get; private set; } = new();
}

public class EntityHistory<TEntity> : EntityHistory<Guid, TEntity>, IEntity
    where TEntity : class, IEntity
{
}