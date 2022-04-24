using uBeac.Repositories;

namespace uBeac;

public static class History
{
    public static async Task Add(object data, string actionName = "None", IApplicationContext context = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var dataType = data.GetType();

        var repositories = GetRepositories(dataType);
        var tasks = repositories.Select(_ => _.Add(data, actionName, context, cancellationToken));
        await Task.WhenAll(tasks);
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