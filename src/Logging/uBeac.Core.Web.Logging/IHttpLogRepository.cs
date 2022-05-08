namespace uBeac.Web.Logging;

public interface IHttpLoggingRepository
{
    Task Create(HttpLog log, CancellationToken cancellationToken = default);
}