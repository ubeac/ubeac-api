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

        public virtual Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            return Repository.Delete(id, cancellationToken);
        }

        public virtual Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            return Repository.DeleteMany(ids, cancellationToken);
        }

        public virtual Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return Repository.GetAll(cancellationToken);
        }

        public virtual Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            return Repository.GetById(id, cancellationToken);
        }

        public virtual Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            return Repository.GetByIds(ids, cancellationToken);
        }

        public virtual Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Repository.Insert(entity, cancellationToken);
        }

        public virtual Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return Repository.InsertMany(entities, cancellationToken);
        }

        public virtual Task<TEntity> Replace(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Repository.Replace(entity, cancellationToken);
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
