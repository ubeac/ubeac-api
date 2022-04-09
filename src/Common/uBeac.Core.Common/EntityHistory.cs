namespace uBeac;

public interface IEntityHistory<TKey, TEntity> : IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    List<TEntity> History { get; }
}

public interface IEntityHistory<TEntity> : IEntityHistory<Guid, TEntity>, IEntity
    where TEntity : class, IEntity
{
}

public class EntityHistory<TKey, TEntity> : Entity<TKey>, IEntityHistory<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    public virtual List<TEntity> History { get; private set; } = new();
}

public class EntityHistory<TEntity> : EntityHistory<Guid, TEntity>, IEntityHistory<TEntity>
    where TEntity : class, IEntity
{
}