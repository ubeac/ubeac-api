using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Repositories.History;

public interface IHistoryManager
{
    Task Write<TData>(TData data, string actionName = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<IHistoryEntity<TData>>> GetAll<TData>(CancellationToken cancellationToken = default);
}

public class HistoryManager : IHistoryManager
{
    protected readonly IServiceProvider Services;

    public HistoryManager(IServiceProvider services)
    {
        Services = services;
    }

    public async Task Write<TData>(TData data, string actionName = null, CancellationToken cancellationToken = default)
    {
        var history = new HistoryEntity<TData>
        {
            Id = Guid.NewGuid(),
            ActionName = actionName,
            CreatedAt = DateTime.Now,
            Context = Services.GetRequiredService<IApplicationContext>()
        };

        await Services.GetRequiredService<IEntityRepository<HistoryEntity<TData>>>().Create(history, cancellationToken);
    }

    public async Task<IEnumerable<IHistoryEntity<TData>>> GetAll<TData>(CancellationToken cancellationToken = default)
        => await Services.GetRequiredService<IEntityRepository<HistoryEntity<TData>>>().GetAll(cancellationToken);
}