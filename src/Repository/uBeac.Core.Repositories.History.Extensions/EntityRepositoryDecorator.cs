using System.Linq.Expressions;

namespace uBeac.Repositories.History;

public class EntityRepositoryHistoryDecorator<TKey, TEntity> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    protected readonly IEntityRepository<TKey, TEntity> Repository;
    protected readonly IHistoryManager History;

    public EntityRepositoryHistoryDecorator(IEntityRepository<TKey, TEntity> repository, IHistoryManager history)
    {
        Repository = repository;
        History = history;
    }

    public async Task Create(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        await Repository.Create(entity, actionName, cancellationToken);
        await History.Write(entity, actionName, cancellationToken);
    }

    public async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Repository.Create(entity, cancellationToken);
        await History.Write(entity, nameof(Create), cancellationToken);
    }

    public async Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default)
    {
        await Repository.Update(entity, actionName, cancellationToken);
        await History.Write(entity, actionName, cancellationToken);
    }

    public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Repository.Update(entity, cancellationToken);
        await History.Write(entity, nameof(Update), cancellationToken);
    }

    public async Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default)
    {
        await Repository.Delete(id, actionName, cancellationToken);
        await History.Write(id, actionName, cancellationToken);
    }

    public async Task Delete(TKey id, CancellationToken cancellationToken = default)
    {
        await Repository.Delete(id, cancellationToken);
        await History.Write(id, nameof(Delete), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        => await Repository.GetAll(cancellationToken);

    public async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        => await Repository.GetById(id, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        => await Repository.GetByIds(ids, cancellationToken);

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        => await Repository.Find(filter, cancellationToken);

    public IQueryable<TEntity> AsQueryable() => Repository.AsQueryable();
}

public class EntityRepositoryHistoryDecorator<TEntity> : EntityRepositoryHistoryDecorator<Guid, TEntity>
    where TEntity : IEntity
{
    public EntityRepositoryHistoryDecorator(IEntityRepository<Guid, TEntity> repository, IHistoryManager history) : base(repository, history)
    {
    }
}