namespace uBeac.Repositories.Events;

public class SetAuditPropertiesEventHandler : IGeneralEntityEventHandler
{
    protected readonly IApplicationContext AppContext;

    public SetAuditPropertiesEventHandler(IApplicationContext appContext)
    {
        AppContext = appContext;
    }

    public async Task OnCreating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        if (entity is IAuditEntity<TKey> audit)
        {
            var now = DateTime.Now;
            audit.CreatedBy = AppContext.UserName;
            audit.CreatedAt = now;
            audit.LastUpdatedBy = AppContext.UserName;
            audit.LastUpdatedAt = now;
        }

        await Task.CompletedTask;
    }

    public async Task OnCreated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnUpdating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        if (entity is IAuditEntity<TKey> audit)
        {
            audit.LastUpdatedBy = AppContext.UserName;
            audit.LastUpdatedAt = DateTime.Now;
        }

        await Task.CompletedTask;
    }

    public async Task OnUpdated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnDeleting<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnDeleted<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }
}