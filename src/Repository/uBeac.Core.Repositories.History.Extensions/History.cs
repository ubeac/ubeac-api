using uBeac.Repositories;

namespace uBeac;

public static class History
{
    public static async Task Add(object data, string dataId = null, string actionName = "None", IApplicationContext context = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var dataType = data.GetType();

        var repositories = GetRepositories(dataType);
        var tasks = repositories.Select(_ => _.Add(data, dataId, actionName, context, cancellationToken));
        await Task.WhenAll(tasks);
    }

    public static async Task<IEnumerable<HistoryEntity<TDataType>>> GetAll<TDataType>(string dataId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var repositories = GetRepositories(typeof(TDataType));
        var tasks = repositories.Select(_ => _.GetAll<TDataType>(dataId, cancellationToken));
        return (await Task.WhenAll(tasks)).SelectMany(_ => _);
    }

    public static async Task<IEnumerable<HistoryEntity>> GetAll(Type dataType, string dataId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var repositories = GetRepositories(dataType);
        var tasks = repositories.Select(_ => _.GetAll(dataType, dataId, cancellationToken));
        return (await Task.WhenAll(tasks)).SelectMany(_ => _);
    }

    internal static IDictionary<Guid, IList<IHistoryRepository>> Repositories { get; } = new Dictionary<Guid, IList<IHistoryRepository>>();

    internal static void AddRepository(Type dataType, IHistoryRepository repository)
    {
        var key = GetTypeKey(dataType);

        if (Repositories.ContainsKey(key) is false)
        {
            var value = new List<IHistoryRepository>();
            Repositories.Add(key, value);
        }

        Repositories[key].Add(repository);
    }

    internal static IList<IHistoryRepository> GetRepositories(Type dataType)
    {
        var key = GetTypeKey(dataType);

        if (Repositories.ContainsKey(key) is false)
        {
            return dataType == typeof(object) ? new List<IHistoryRepository>() : GetRepositories(typeof(object));
        }

        return Repositories[key];
    }

    internal static Guid GetTypeKey(Type dataType) => dataType.GUID;
}