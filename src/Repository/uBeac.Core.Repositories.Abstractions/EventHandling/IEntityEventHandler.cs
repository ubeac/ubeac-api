namespace uBeac.Repositories;

public interface IEntityEventHandler
{
}

public interface IEntityEventHandler<TKey, in TEntity> : IEntityEventHandler
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    Task OnCreating(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);
    Task OnCreated(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);

    Task OnUpdating(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);
    Task OnUpdated(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);

    Task OnDeleting(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);
    Task OnDeleted(TEntity entity, string actionName = null, CancellationToken cancellationToken = default);
}

public interface IGeneralEntityEventHandler : IEntityEventHandler
{
    Task OnCreating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;

    Task OnCreated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;

    Task OnUpdating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;

    Task OnUpdated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;

    Task OnDeleting<TKey, TEntity>(TEntity entity,string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;

    Task OnDeleted<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>;
}