﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using uBeac.Repositories;
using uBeac.Repositories.History;

namespace uBeac.Core.Repositories.EntityFramework
{
    public class EFEntityRepository<TKey, TEntity, TContext> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
    where TContext : DbContext
    {
        protected readonly TContext DbContext;
        protected readonly IApplicationContext ApplicationContext;
        protected readonly HistoryFactory HistoryFactory;
        protected readonly DbSet<TEntity> Entities;
        public EFEntityRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory)
        {
            DbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
            ApplicationContext = applicationContext;
            HistoryFactory = historyFactory;
        }

        public IQueryable<TEntity> AsQueryable() => Entities.AsNoTracking();

        protected virtual async Task AddToHistory(TEntity entity, string actionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var historyRepositories = HistoryFactory.GetRepositories<TEntity>();

            foreach (var repository in historyRepositories)
            {
                await repository.Add(entity, actionName, cancellationToken);
            }
        }

        public virtual async Task Create(TEntity entity, string actionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // If the entity is extend from IAuditEntity, the audit properties (CreatedAt, CreatedBy, etc.) should be set
            if (entity is IAuditEntity<TKey> audit) SetPropertiesOnCreate(audit, ApplicationContext);

            await Entities.AddAsync(entity, cancellationToken);

            await AddToHistory(entity, actionName, cancellationToken);
        }

        public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Create(entity, nameof(Create), cancellationToken);
        }

        public virtual async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await Entities.SingleOrDefaultAsync(s => s.Id.Equals(id));
            Entities.Remove(entity);

            await AddToHistory(entity, actionName, cancellationToken);
        }

        public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
        {
            await Delete(id, nameof(Delete), cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = AsQueryable().Where(filter);
            return await findResult.ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.Run(() => AsQueryable().AsEnumerable());
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Entities.SingleOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => AsQueryable().Where(s => ids.Contains(s.Id)).AsEnumerable());
        }

        public virtual async Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // If the entity is extend from IAuditEntity, the audit properties (LastUpdatedAt, LastUpdatedBy, etc.) should be set
            if (entity is IAuditEntity<TKey> audit) SetPropertiesOnUpdate(audit, ApplicationContext);

            await Task.Run(() => DbContext.Entry(entity).State = EntityState.Modified);

            await AddToHistory(entity, actionName, cancellationToken);
        }

        public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Update(entity, nameof(Update), cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DbContext.SaveChangesAsync();
        }

        private void SetPropertiesOnCreate(IAuditEntity<TKey> entity, IApplicationContext appContext)
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;

            entity.CreatedAt = now;
            entity.CreatedBy = userName;
            entity.LastUpdatedAt = now;
            entity.LastUpdatedBy = userName;
        }

        private void SetPropertiesOnUpdate(IAuditEntity<TKey> entity, IApplicationContext appContext)
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;

            entity.LastUpdatedAt = now;
            entity.LastUpdatedBy = userName;
        }
    }
}