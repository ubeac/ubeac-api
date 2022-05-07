using Microsoft.Extensions.DependencyInjection;

namespace uBeac.Repositories.History;

public class HistoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public HistoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<IHistoryRepository> GetRepositories<T>()
    {
        var registeredTypes = _serviceProvider.GetService<RegisteredTypesForHistory>();

        var result = new List<IHistoryRepository>();
        if (registeredTypes.ContainsKey(typeof(T)))
        {
            foreach (var type in registeredTypes[typeof(T)])
            {
                result.Add((IHistoryRepository)_serviceProvider.GetRequiredService(type));
            }
        }

        return result;
    }

}
