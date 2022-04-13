namespace uBeac.Services;

public interface IService
{
}

public interface IEntityService<TKey, TEntity> : IService
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    Task Create(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}

public interface IEntityService<TEntity> : IEntityService<Guid, TEntity>
    where TEntity : IEntity
{
}