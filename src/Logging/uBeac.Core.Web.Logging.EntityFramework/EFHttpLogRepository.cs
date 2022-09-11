namespace uBeac.Web.Logging.EntityFramework;

public class EFHttpLogRepository<TContext> : IHttpLogRepository
    where TContext : HttpLogDbContext
{
    protected readonly TContext Context;

    public EFHttpLogRepository(TContext context)
    {
        Context = context;
    }

    public async Task Create(HttpLog log, CancellationToken cancellationToken = default)
    {
        var table = log.StatusCode switch
        {
            < 500 and >= 400 => Context.HttpLogs4xx,
            >= 500 => Context.HttpLogs5xx,
            _ => Context.HttpLogs2xx
        };

        await table.AddAsync(log, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}