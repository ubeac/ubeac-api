using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace uBeac.Repositories.EntityFramework;

public class EFEntityRepository<TKey, TEntity, TContext> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
    where TContext : DbContext
{
    protected readonly TContext DbContext;
    protected readonly IApplicationContext ApplicationContext;
    protected readonly IEntityEventManager<TKey, TEntity> EventManager;
    protected readonly DbSet<TEntity> DbSet;

    public EFEntityRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TKey, TEntity> eventManager)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
        ApplicationContext = applicationContext;
        EventManager = eventManager;
    }

    public IQueryable<TEntity> AsQueryable() => DbSet.AsNoTracking();

    public virtual async Task Create(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await EventManager.OnCreating(entity, actionName, cancellationToken);

        await DbSet.AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        await EventManager.OnCreated(entity, actionName, cancellationToken);
    }

    public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Create(entity, nameof(Create), cancellationToken);
    }

    public async Task Delete(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await EventManager.OnDeleting(entity, actionName, cancellationToken);

        DbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);

        await EventManager.OnDeleted(entity, actionName, cancellationToken);
    }

    public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Delete(entity, nameof(Delete), cancellationToken);
    }

    public virtual async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await GetById(id, cancellationToken);

        await EventManager.OnDeleting(entity, actionName, cancellationToken);

        DbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);

        await EventManager.OnDeleted(entity, actionName, cancellationToken);
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

        await EventManager.OnUpdating(entity, actionName, cancellationToken);

        DbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);

        await EventManager.OnUpdated(entity, actionName, cancellationToken);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Update(entity, nameof(Update), cancellationToken);
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}

public class EFEntityRepository<TEntity, TContext> : EFEntityRepository<Guid, TEntity, TContext>, IEntityRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : EFDbContext
{
    public EFEntityRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TEntity> historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}