using System.Linq.Expressions;

namespace uBeac.Repositories;

public interface IRepository
{
}

public interface IEntityRepository<TKey, TEntity> : IRepository
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    Task Create(TEntity entity, string actionName, CancellationToken cancellationToken = default);
    Task Create(TEntity entity, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity, string actionName, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    IQueryable<TEntity> AsQueryable();
}

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity>
    where TEntity : IEntity
{
}

