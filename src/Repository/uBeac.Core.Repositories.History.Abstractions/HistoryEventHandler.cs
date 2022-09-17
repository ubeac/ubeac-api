namespace uBeac.Repositories.History;

public class HistoryEventHandler : IGeneralEntityEventHandler
{
    protected readonly IHistoryManager HistoryManager;

    public HistoryEventHandler(IHistoryManager historyManager)
    {
        HistoryManager = historyManager;
    }

    public async Task OnCreating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnCreated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await HistoryManager.Write(entity, actionName, cancellationToken);
    }

    public async Task OnUpdating<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnUpdated<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await HistoryManager.Write(entity, actionName, cancellationToken);
    }

    public async Task OnDeleting<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await Task.CompletedTask;
    }

    public async Task OnDeleted<TKey, TEntity>(TEntity entity, string actionName = null, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IEntity<TKey>
    {
        await HistoryManager.Write(entity, actionName, cancellationToken);
    }
}