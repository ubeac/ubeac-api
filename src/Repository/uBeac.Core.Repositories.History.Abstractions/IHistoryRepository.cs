namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task Add<T>(T data, string actionName, CancellationToken cancellationToken = default);
}

public interface IEntityHistoryRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
}

public interface IEntityHistoryRepository<TEntity> : IEntityHistoryRepository<Guid, TEntity>, IEntityRepository<TEntity>
    where TEntity : IEntity
{
}