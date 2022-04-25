using uBeac.Repositories.MongoDB;

namespace uBeac.Web.Logging.MongoDB;

public class MongoHttpLogRepository<TKey, THttpLog, TContext> : MongoEntityRepository<TKey, THttpLog, TContext>, IHttpLogRepository<TKey, THttpLog>
    where TKey : IEquatable<TKey>
    where THttpLog : HttpLog<TKey>
    where TContext : IMongoDBContext
{
    public MongoHttpLogRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}

public class MongoHttpLogRepository<THttpLog, TContext> : MongoHttpLogRepository<Guid, THttpLog, TContext>, IHttpLogRepository<THttpLog>
    where THttpLog : HttpLog
    where TContext : IMongoDBContext
{
    public MongoHttpLogRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}

public class MongoHttpLogRepository<TContext> : MongoHttpLogRepository<HttpLog, TContext>, IHttpLogRepository
    where TContext : IMongoDBContext
{
    public MongoHttpLogRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}