﻿using System.Linq.Expressions;

namespace uBeac.Repositories
{
    public interface IRepository 
    {
    }

    public interface IEntityRepository<TKey, TEntity>: IRepository
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>
    {
        Task Create(TEntity entity, CreateEntityOptions? options = null, CancellationToken cancellationToken = default);
        Task<TEntity> Update(TEntity entity, UpdateEntityOptions? options = null, CancellationToken cancellationToken = default);
        Task<bool> Delete(TKey id, DeleteEntityOptions? options = null, CancellationToken cancellationToken = default);
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
}
