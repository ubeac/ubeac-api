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
        switch (log.StatusCode)
        {
            case < 500 and >= 400:
                await Context.HttpLogs4xx.AddAsync(new EFHttpLog4xx(log), cancellationToken);
                break;

            case >= 500:
                await Context.HttpLogs5xx.AddAsync(new EFHttpLog5xx(log), cancellationToken);
                break;

            default:
                await Context.HttpLogs2xx.AddAsync(new EFHttpLog2xx(log), cancellationToken);
                break;
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
}