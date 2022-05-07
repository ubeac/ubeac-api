namespace Microsoft.Extensions.DependencyInjection;

public class HistoryBuilder
{
    private readonly IServiceCollection _services;
    // the key is IEntityHistoryRepository
    private readonly List<Type> _registeredTypesForHistory;
    private readonly Type _historyRepositoryType;

    public HistoryBuilder(IServiceCollection services, Type historyRepositoryType)
    {
        _services = services;
        _registeredTypesForHistory = new List<Type>();
        _historyRepositoryType = historyRepositoryType;
    }

    public HistoryBuilder For<T>()
    {
        _registeredTypesForHistory.Add(typeof(T));
        return this;
    }

    public void Register()
    {
        var registeredTypesForHistory = GetRegisteredTypesForHistory();
        foreach (var type in _registeredTypesForHistory)
        {
            if (!registeredTypesForHistory.ContainsKey(type))
                registeredTypesForHistory.Add(type, new List<Type>());
            registeredTypesForHistory[type].Add(_historyRepositoryType);
        }
    }

    private RegisteredTypesForHistory GetRegisteredTypesForHistory()
    {
        foreach (var item in _services)
        {
            if (item.ServiceType == typeof(RegisteredTypesForHistory))
                return (RegisteredTypesForHistory)item.ImplementationInstance;
        }

        var newRegistration = new RegisteredTypesForHistory();
        _services.AddSingleton(newRegistration);

        return newRegistration;
    }

}
