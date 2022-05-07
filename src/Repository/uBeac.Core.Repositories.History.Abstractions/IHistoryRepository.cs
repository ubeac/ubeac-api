namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task Add<T>(T data, string actionName, CancellationToken cancellationToken = default);
}