using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Web.Logging.EntityFramework;

public class HttpLogEntityConfiguration<THttpLog> : IEntityTypeConfiguration<THttpLog>
    where THttpLog : HttpLog
{
    protected readonly Type AppContextType;

    public HttpLogEntityConfiguration(IApplicationContext appContext)
    {
        AppContextType = appContext.GetType();
    }

    public virtual void Configure(EntityTypeBuilder<THttpLog> builder)
    {
        builder.OwnsOne(x => x.Exception);

        builder.OwnsOne(x => x.Request)
            .Property(x => x.Headers)
            .HasJsonConversion();

        builder.OwnsOne(x => x.Response)
            .Property(x => x.Headers)
            .HasJsonConversion();

        builder.Property(x => x.Context)
            .HasAppContextJsonConversion(AppContextType);
    }
}