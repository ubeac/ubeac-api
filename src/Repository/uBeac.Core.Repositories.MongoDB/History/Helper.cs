namespace uBeac.Repositories;

public static class History
{
    public static async Task AddToHistory(object data, string actionName = "None", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var dataType = data.GetType();

        var repository = GetRepository(dataType);
        await repository.AddToHistory(data, actionName, cancellationToken);
    }

    internal static List<KeyValuePair<Type, IHistoryRepository>> Repositories { get; } = new();

    internal static void AddRepository(Type dataType, IHistoryRepository repository)
    {
        Repositories.Add(new KeyValuePair<Type, IHistoryRepository>(dataType, repository));
    }

    internal static IHistoryRepository GetRepository(Type dataType)
        => Repositories.FirstOrDefault(_ => _.Key == dataType).Value ?? Repositories.First().Value;
}