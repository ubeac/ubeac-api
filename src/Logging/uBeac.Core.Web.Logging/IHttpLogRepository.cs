namespace uBeac.Web.Logging;

public interface IHttpLogRepository
{
    Task Create(HttpLog log, CancellationToken cancellationToken = default);
}