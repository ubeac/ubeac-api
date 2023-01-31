using MongoDB.Driver;
using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.BackgroundTasks.Logging.MongoDB;

public class MongoBackgroundTaskLogRepository<TEntity, TContext> : MongoEntityRepository<TEntity, TContext>, IBackgroundTaskLogRepository<TEntity>
    where TEntity : BackgroundTaskLog
    where TContext : IMongoDBContext
{
    public MongoBackgroundTaskLogRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<IEnumerable<TEntity>> Search(BackgroundTaskSearchRequest request, CancellationToken cancellationToken = default)
    {
        var builder = Builders<TEntity>.Filter;

        var filter = builder.Empty;

        if (request.FromDate.HasValue) filter &= builder.Gte(x => x.StartDate, request.FromDate);
        if (request.ToDate.HasValue) filter &= builder.Lte(x => x.StartDate, request.ToDate);

        if (!string.IsNullOrWhiteSpace(request.Term)) filter &= builder.Contains(x => x.Option.Name, request.Term) |
                                                                builder.Contains(x => x.Option.Type, request.Term);

        return (await Collection.FindAsync<TEntity>(filter, null, cancellationToken)).ToList(cancellationToken);
    }
}

public class MongoBackgroundTaskLogRepository<TContext> : MongoBackgroundTaskLogRepository<BackgroundTaskLog, TContext>, IBackgroundTaskLogRepository
    where TContext : IMongoDBContext
{
    public MongoBackgroundTaskLogRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }
}
