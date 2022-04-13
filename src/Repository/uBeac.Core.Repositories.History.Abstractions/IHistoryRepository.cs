namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task AddToHistory(object data, string actionName = "None", CancellationToken cancellationToken = default);
    Task AddToHistory<TData>(TData data, string actionName = "None", CancellationToken cancellationToken = default);
}