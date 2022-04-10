namespace uBeac.Repositories;

public interface IHistoryRepository<in TData> : IRepository
    where TData : class
{
    Task AddToHistory(TData data, string actionName = "None", CancellationToken cancellationToken = default);
}