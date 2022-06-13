﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using uBeac.Repositories.History;

namespace uBeac.Repositories.EntityFramework;

public class EFEntityRepository<TKey, TEntity, TContext> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
    where TContext : EFDbContext
{
    protected readonly TContext DbContext;
    protected readonly IApplicationContext ApplicationContext;
    protected readonly HistoryFactory HistoryFactory;
    protected readonly DbSet<TEntity> DbSet;

    public EFEntityRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
        ApplicationContext = applicationContext;
        HistoryFactory = historyFactory;
    }

    public IQueryable<TEntity> AsQueryable() => DbSet.AsNoTracking();

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

        await DbSet.AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        await AddToHistory(entity, actionName, cancellationToken);
    }

    public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Create(entity, nameof(Create), cancellationToken);
    }

    public virtual async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await GetById(id, cancellationToken);
        DbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);

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

        return await Task.Run(() => AsQueryable().AsEnumerable(), cancellationToken);
    }

    public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await DbSet.FindAsync(id) ?? throw new NullReferenceException("Entity is not found.");
    }

    public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Task.Run(() => AsQueryable().Where(entity => ids.Contains(entity.Id)).AsEnumerable(), cancellationToken);
    }

    public virtual async Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // If the entity is extend from IAuditEntity, the audit properties (LastUpdatedAt, LastUpdatedBy, etc.) should be set
        if (entity is IAuditEntity<TKey> audit) SetPropertiesOnUpdate(audit, ApplicationContext);

        DbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);

        await AddToHistory(entity, actionName, cancellationToken);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Update(entity, nameof(Update), cancellationToken);
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
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