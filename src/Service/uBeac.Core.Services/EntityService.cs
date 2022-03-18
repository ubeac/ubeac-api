using uBeac.Repositories;

namespace uBeac.Services
{
    public class EntityService<TKey, TEntity> : IEntityService<TKey, TEntity>
       where TKey : IEquatable<TKey>
       where TEntity : IEntity<TKey>
    {
        protected readonly IEntityRepository<TKey, TEntity> Repository;
        protected readonly IApplicationContext AppContext;

        public EntityService(IEntityRepository<TKey, TEntity> repository, IApplicationContext appContext)
        {
            Repository = repository;
            AppContext = appContext;
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

        public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            // If the entity is extend from IAuditEntity, the audit properties (CreatedAt, CreatedBy, etc.) should be set
            SetAuditPropsOnCreate(entity);

            await Repository.Create(entity, cancellationToken);
        }

        public virtual async Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            // If the entities is extend from IAuditEntity, the audit properties (CreatedAt, CreatedBy, etc.) should be set
            foreach (var entity in entities) SetAuditPropsOnCreate(entity);

            await Repository.CreateMany(entities, cancellationToken);
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            // If the entity is extend from IAuditEntity, the audit properties (LastUpdatedAt, LastUpdatedBy, etc.) should be set
            SetAuditPropsOnUpdate(entity);

            return await Repository.Update(entity, cancellationToken);
        }

        protected virtual void SetAuditPropsOnCreate(TEntity entity)
        {
            // Set audit properties (CreatedAt, CreatedBy, etc.)
            if (entity is IAuditEntity<TKey> audit)
            {
                audit.CreatedAt = AppContext.Time;
                audit.CreatedBy = AppContext.UserName;
            }
        }

        protected virtual void SetAuditPropsOnUpdate(TEntity entity)
        {
            // Set audit properties (LastUpdatedAt, LastUpdatedBy, etc.)
            if (entity is IAuditEntity<TKey> audit)
            {
                audit.LastUpdatedAt = AppContext.Time;
                audit.LastUpdatedBy = AppContext.UserName;
            }
        }
    }

    public class EntityService<TEntity> : EntityService<Guid, TEntity>, IEntityService<TEntity>
        where TEntity : IEntity
    {
        public EntityService(IEntityRepository<TEntity> repository, IApplicationContext appContext) : base(repository, appContext)
        {
        }
    }
}
