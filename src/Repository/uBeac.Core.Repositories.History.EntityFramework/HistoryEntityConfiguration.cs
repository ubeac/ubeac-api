using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Repositories.History.EntityFramework;

public class HistoryEntityConfiguration<TKey, TEntity> : IEntityTypeConfiguration<TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : HistoryEntity<TKey>
{
    protected readonly Type AppContextType;

    public HistoryEntityConfiguration(IApplicationContext appContext)
    {
        AppContextType = appContext.GetType();
    }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Data)
            .HasJsonConversion();

        builder.Property(x => x.Context)
            .HasAppContextJsonConversion(AppContextType);
    }
}

public class HistoryEntityConfiguration<TEntity> : HistoryEntityConfiguration<Guid, TEntity>
    where TEntity : HistoryEntity
{
    public HistoryEntityConfiguration(IApplicationContext appContext) : base(appContext)
    {
    }
}

public class HistoryEntityConfiguration : HistoryEntityConfiguration<HistoryEntity>
{
    public HistoryEntityConfiguration(IApplicationContext appContext) : base(appContext)
    {
    }
}