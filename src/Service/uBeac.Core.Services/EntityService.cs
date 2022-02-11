using uBeac.Repositories;

namespace uBeac.Services
{
    public class EntityService<TKey, TEntity> : IEntityService<TKey, TEntity>
       where TKey : IEquatable<TKey>
       where TEntity : IEntity<TKey>
    {
        protected readonly IEntityRepository<TKey, TEntity> Repository;

        public EntityService(IEntityRepository<TKey, TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            return await Repository.Delete(id, cancellationToken);
        }

        public virtual async Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            return await Repository.DeleteMany(ids, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Repository.GetAll(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            return await Repository.GetById(id, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIds(ids, cancellationToken);
        }

        public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.Insert(entity, cancellationToken);
        }

        public virtual async Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Repository.InsertMany(entities, cancellationToken);
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Repository.Update(entity, cancellationToken);
        }
    }

    public class EntityService<TEntity> : EntityService<Guid, TEntity>, IEntityService<TEntity>
        where TEntity : IEntity
    {
        public EntityService(IEntityRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
