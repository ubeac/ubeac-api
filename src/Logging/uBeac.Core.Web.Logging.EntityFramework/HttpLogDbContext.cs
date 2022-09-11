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
        modelBuilder.ApplyConfiguration(new HttpLogEntityConfiguration(AppContext));
        base.OnModelCreating(modelBuilder);
    }
    
    public virtual DbSet<HttpLog> HttpLogs2xx { get; set; }
    public virtual DbSet<HttpLog> HttpLogs4xx { get; set; }
    public virtual DbSet<HttpLog> HttpLogs5xx { get; set; }
}