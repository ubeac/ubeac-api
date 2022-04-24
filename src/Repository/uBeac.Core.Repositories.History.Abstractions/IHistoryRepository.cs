namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task Add(object data, string actionName = "None", IApplicationContext? context = null, CancellationToken cancellationToken = default);
}