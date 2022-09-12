namespace uBeac.Repositories.History.EntityFramework;

public interface IEFHistoryRepository : IHistoryRepository
{
}

public class EFHistoryRepository<TKey, THistory, TContext> : IEFHistoryRepository
    where TKey : IEquatable<TKey>
    where THistory : HistoryEntity<TKey>, new()
    where TContext : HistoryDbContext<TKey, THistory>
{
    protected readonly IApplicationContext ApplicationContext;
    protected readonly TContext Context;

    public EFHistoryRepository(IApplicationContext applicationContext, TContext context)
    {
        ApplicationContext = applicationContext;
        Context = context;
    }

    public async Task Add<T>(T data, string actionName, CancellationToken cancellationToken = default)
    {
        var history = new THistory
        {
            Data = data,
            ActionName = actionName,
            Context = ApplicationContext,
            CreatedAt = DateTime.Now
        };

        await Context.History.AddAsync(history, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}

public class EFHistoryRepository<THistory, TContext> : EFHistoryRepository<Guid, THistory, TContext>
    where THistory : HistoryEntity, new()
    where TContext : HistoryDbContext<THistory>
{
    public EFHistoryRepository(IApplicationContext applicationContext, TContext context) : base(applicationContext, context)
    {
    }
}

public class EFHistoryRepository<TContext> : EFHistoryRepository<Guid, HistoryEntity, TContext>
    where TContext : HistoryDbContext
{
    public EFHistoryRepository(IApplicationContext applicationContext, TContext context) : base(applicationContext, context)
    {
    }
}

public class EFHistoryRepository : EFHistoryRepository<HistoryDbContext>
{
    public EFHistoryRepository(IApplicationContext applicationContext, HistoryDbContext context) : base(applicationContext, context)
    {
    }
}