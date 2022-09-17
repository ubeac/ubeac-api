using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;

namespace API;

public class HistoryDbContext : EFDbContext
{
    protected readonly Type AppContextType;

    public HistoryDbContext(DbContextOptions options, IApplicationContext appContext) : base(options)
    {
        AppContextType = appContext.GetType();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoryEntity<User>>()
            .Property(x => x.Context)
            .HasAppContextJsonConversion(AppContextType);

        modelBuilder.Entity<HistoryEntity<User>>()
            .OwnsOne(x => x.Data);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<HistoryEntity<User>> Users { get; set; }
}