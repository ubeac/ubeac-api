namespace uBeac.Repositories;

public interface IHistoryRepository : IRepository
{
    Task Add<TData>(TData data, string dataId = null, string actionName = "None", IApplicationContext context = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<HistoryEntity<TData>>> GetAll<TData>(string dataId, CancellationToken cancellationToken = default);
    Task<IEnumerable<HistoryEntity>> GetAll(Type dataType, string dataId, CancellationToken cancellationToken);
}