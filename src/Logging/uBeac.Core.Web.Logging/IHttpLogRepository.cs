using uBeac.Repositories;

namespace uBeac.Web.Logging;

public interface IHttpLogRepository<TKey, THttpLog> : IEntityRepository<TKey, THttpLog>
    where TKey : IEquatable<TKey>
    where THttpLog : HttpLog<TKey>
{
}

public interface IHttpLogRepository<THttpLog> : IHttpLogRepository<Guid, THttpLog>, IEntityRepository<THttpLog>
    where THttpLog : HttpLog
{
}

public interface IHttpLogRepository : IHttpLogRepository<HttpLog>
{
}