using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace uBeac.Repositories.History;

public interface IHistoryManager
{
    Task Write<TData>(TData data, string actionName = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<IHistoryEntity<TData>>> GetAll<TData>(CancellationToken cancellationToken = default);
}

public class HistoryManager : IHistoryManager
{
    protected readonly IServiceProvider Services;
    protected readonly ILogger<HistoryManager> Logger;

    public HistoryManager(IServiceProvider services, ILogger<HistoryManager> logger)
    {
        Services = services;
        Logger = logger;
    }

    public async Task Write<TData>(TData data, string actionName = null, CancellationToken cancellationToken = default)
    {
        var history = new HistoryEntity<TData>
        {
            Id = Guid.NewGuid(),
            ActionName = actionName,
            CreatedAt = DateTime.Now,
            Context = Services.GetRequiredService<IApplicationContext>(),
            Data = data
        };

        var repository = Services.GetService<IEntityRepository<HistoryEntity<TData>>>();
        if (repository != null)
        {
            try
            {
                await repository.Create(history, cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
        }
    }

    public async Task<IEnumerable<IHistoryEntity<TData>>> GetAll<TData>(CancellationToken cancellationToken = default)
        => await Services.GetRequiredService<IEntityRepository<HistoryEntity<TData>>>().GetAll(cancellationToken);
}