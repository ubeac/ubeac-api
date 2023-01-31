using uBeac.Services;

namespace uBeac.BackgroundTasks.Logging;

public interface IBackgroundTaskLogService<TEntity> : IEntityService<TEntity>
    where TEntity : BackgroundTaskLog
{
    Task<IListResult<TEntity>> Search(BackgroundTaskSearchRequest request, CancellationToken cancellationToken = default);

    Task Update(TEntity log, string description, bool? success = null, CancellationToken cancellationToken = default);
}

public interface IBackgroundTaskLogService : IBackgroundTaskLogService<BackgroundTaskLog>
{
    Task<BackgroundTaskLog> Create(RecurringBackgroundTaskOption option, CancellationToken cancellationToken = default);
}

public class BackgroundTaskLogService<TEntity> : EntityService<TEntity>, IBackgroundTaskLogService<TEntity>
    where TEntity : BackgroundTaskLog
{
    private readonly IBackgroundTaskLogRepository<TEntity> _repository;

    public BackgroundTaskLogService(IBackgroundTaskLogRepository<TEntity> repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<IListResult<TEntity>> Search(BackgroundTaskSearchRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _repository.Search(request, cancellationToken);

        return result.ToListResult();
    }

    public async Task Update(TEntity log, string description, bool? success = null, CancellationToken cancellationToken = default)
    {
        if (success.HasValue && success.Value)
        {
            log.Succeed += 1;
        }
        else if (success.HasValue)
        {
            log.Failure += 1;
        }

        log.Descriptions.Add($"{description} at {DateTime.Now}.\n");

        await Task.Factory.StartNew(async () => await Update(log, cancellationToken), cancellationToken);
    }
}

public class BackgroundTaskLogService : BackgroundTaskLogService<BackgroundTaskLog>, IBackgroundTaskLogService
{
    public BackgroundTaskLogService(IBackgroundTaskLogRepository repository) : base(repository)
    {
    }

    public async Task<BackgroundTaskLog> Create(RecurringBackgroundTaskOption option, CancellationToken cancellationToken = default)
    {
        var log = new BackgroundTaskLog
        {
            Descriptions = new List<string> { "Background Task is started!" },
            StartDate = DateTime.Now,
            Status = BackgroundTaskStatus.Running,
            Option = option
        };

        await Task.Factory.StartNew(async () => await Create(log, cancellationToken), cancellationToken);

        return log;
    }
}
