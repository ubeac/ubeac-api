using Microsoft.EntityFrameworkCore;

namespace uBeac.Web.Logging.EntityFramework;

public class HttpLogDbContext : DbContext
{
    protected readonly IApplicationContext AppContext;

    public HttpLogDbContext(DbContextOptions<HttpLogDbContext> options, IApplicationContext appContext) : base(options)
    {
        AppContext = appContext;
    }

    protected HttpLogDbContext(IApplicationContext appContext)
    {
        AppContext = appContext;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HttpLogEntityConfiguration<EFHttpLog2xx>(AppContext));
        modelBuilder.ApplyConfiguration(new HttpLogEntityConfiguration<EFHttpLog4xx>(AppContext));
        modelBuilder.ApplyConfiguration(new HttpLogEntityConfiguration<EFHttpLog5xx>(AppContext));

        base.OnModelCreating(modelBuilder);
    }
    
    public virtual DbSet<EFHttpLog2xx> HttpLogs2xx { get; set; }
    public virtual DbSet<EFHttpLog4xx> HttpLogs4xx { get; set; }
    public virtual DbSet<EFHttpLog5xx> HttpLogs5xx { get; set; }
}