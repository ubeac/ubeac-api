using uBeac.Repositories;

namespace uBeac.BackgroundTasks.Logging;

public interface IBackgroundTaskLogRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : BackgroundTaskLog
{
    Task<IEnumerable<TEntity>> Search(BackgroundTaskSearchRequest request, CancellationToken cancellationToken = default);
}

public interface IBackgroundTaskLogRepository : IBackgroundTaskLogRepository<BackgroundTaskLog>
{
}
