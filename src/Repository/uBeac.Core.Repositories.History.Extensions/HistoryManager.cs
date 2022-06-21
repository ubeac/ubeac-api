using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Repositories.History;

public interface IHistoryManager
{
    Task Write<TData>(TData data, string actionName, CancellationToken cancellationToken = default);
}

public class HistoryManager : IHistoryManager
{
    protected readonly IServiceProvider ServiceProvider;

    public HistoryManager(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual async Task Write<TData>(TData data, string actionName, CancellationToken cancellationToken = default)
    {
        var typesDictionary = GetTypesDictionary();
        var repositories = GetRepositories<TData>(typesDictionary);
        var tasks = repositories.Select(repo => repo.Add(data, actionName, cancellationToken));
        await Task.WhenAll(tasks);
    }

    protected IHistoryTypesDictionary GetTypesDictionary() => ServiceProvider.GetService<IHistoryTypesDictionary>();

    protected IEnumerable<IHistoryRepository> GetRepositories<TData>(IHistoryTypesDictionary typesDictionary)
    {
        var dataType = typeof(TData);

        if (typesDictionary?.ContainsKey(dataType) is not true) return new List<IHistoryRepository>();

        return typesDictionary[dataType].Select(repositoryType => (IHistoryRepository)ServiceProvider.GetRequiredService(repositoryType));
    }
}