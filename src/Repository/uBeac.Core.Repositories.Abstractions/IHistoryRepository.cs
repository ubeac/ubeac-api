namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task AddToHistory(object data, string actionName = "None", CancellationToken cancellationToken = default);
}