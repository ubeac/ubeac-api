namespace uBeac.Repositories;

public interface IEntityEventManager
{
}

public interface IEntityEventManager<TKey, in TEntity> : IEntityEventHandler<TKey, TEntity>, IEntityEventManager
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
}

public interface IEntityEventManager<in TEntity> : IEntityEventManager<Guid, TEntity>
    where TEntity : IEntity
{
}

public class EntityEventManager<TKey, TEntity> : IEntityEventManager<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    protected readonly List<IGeneralEntityEventHandler> GeneralEventHandlers;
    protected readonly List<IEntityEventHandler<TKey, TEntity>> EntityEventHandlers;

    public EntityEventManager(IEnumerable<IGeneralEntityEventHandler> generalEventHandlers, IEnumerable<IEntityEventHandler<TKey, TEntity>> entityEventHandlers)
    {
        GeneralEventHandlers = generalEventHandlers.ToList();
        EntityEventHandlers = entityEventHandlers.ToList();
    }

    public async Task OnCreating(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnCreating<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnCreating(entity, actionName, cancellationToken);
    }

    public async Task OnCreated(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnCreated<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnCreated(entity, actionName, cancellationToken);
    }

    public async Task OnUpdating(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnUpdating<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnUpdating(entity, actionName, cancellationToken);
    }

    public async Task OnUpdated(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnUpdated<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnUpdated(entity, actionName, cancellationToken);
    }

    public async Task OnDeleting(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnDeleting<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnDeleting(entity, actionName, cancellationToken);
    }

    public async Task OnDeleted(TEntity entity, string actionName = null, CancellationToken cancellationToken = default)
    {
        foreach (var handler in GeneralEventHandlers) await handler.OnDeleted<TKey, TEntity>(entity, actionName, cancellationToken);
        foreach (var handler in EntityEventHandlers) await handler.OnDeleted(entity, actionName, cancellationToken);
    }
}

public class EntityEventManager<TEntity> : EntityEventManager<Guid, TEntity>, IEntityEventManager<TEntity>
    where TEntity : IEntity
{
    public EntityEventManager(IEnumerable<IGeneralEntityEventHandler> generalEventHandlers, IEnumerable<IEntityEventHandler<Guid, TEntity>> entityEventHandlers) : base(generalEventHandlers, entityEventHandlers)
    {
    }
}