namespace uBeac.Repositories;

public interface IEntityHistoryRepository<TKey, in TEntity> : IRepository
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    Task AddToHistory(TEntity entity, CancellationToken cancellationToken = default);
}

public interface IEntityHistoryRepository<in TEntity> : IEntityHistoryRepository<Guid, TEntity>
    where TEntity : class, IEntity
{
}