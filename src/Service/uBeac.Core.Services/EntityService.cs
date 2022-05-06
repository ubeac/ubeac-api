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

        public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Repository.Delete(id, cancellationToken: cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Repository.GetAll(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Repository.GetById(id, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Repository.GetByIds(ids, cancellationToken);
        }

        public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Repository.Create(entity, cancellationToken: cancellationToken);
        }

        public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Repository.Update(entity, cancellationToken: cancellationToken);
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
