using Microsoft.EntityFrameworkCore;

namespace uBeac.Repositories.History.EntityFramework;

public class HistoryDbContext<TKey, TEntity> : DbContext
    where TKey : IEquatable<TKey>
    where TEntity : HistoryEntity<TKey>
{
    protected readonly IApplicationContext AppContext;

    public HistoryDbContext(DbContextOptions options, IApplicationContext appContext) : base(options)
    {
        AppContext = appContext;
    }

    protected HistoryDbContext(IApplicationContext appContext)
    {
        AppContext = appContext;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HistoryEntityConfiguration<TKey,TEntity>(AppContext));
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<TEntity> History { get; set; }
}

public class HistoryDbContext<TEntity> : HistoryDbContext<Guid, TEntity>
    where TEntity : HistoryEntity
{
    public HistoryDbContext(DbContextOptions options, IApplicationContext appContext) : base(options, appContext)
    {
    }

    public HistoryDbContext(IApplicationContext appContext) : base(appContext)
    {
    }
}

public class HistoryDbContext : HistoryDbContext<HistoryEntity>
{
    public HistoryDbContext(DbContextOptions options, IApplicationContext appContext) : base(options, appContext)
    {
    }

    public HistoryDbContext(IApplicationContext appContext) : base(appContext)
    {
    }
}