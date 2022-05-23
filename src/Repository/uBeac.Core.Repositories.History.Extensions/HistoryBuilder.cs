using uBeac.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public interface IHistoryBuilder
{
    IHistoryBuilder For<T>();
    void Register();
}

public class HistoryBuilder : IHistoryBuilder
{
    protected readonly IServiceCollection Services;

    protected readonly Type RepositoryType;
    protected readonly List<Type> DataTypes;

    public HistoryBuilder(IServiceCollection services, Type repositoryType)
    {
        Services = services;
        DataTypes = new List<Type>();
        RepositoryType = repositoryType;
    }

    public IHistoryBuilder For<T>()
    {
        DataTypes.Add(typeof(T));
        return this;
    }

    public void Register()
    {
        var typesDictionary = GetTypesDictionary();
        foreach (var dataType in DataTypes) typesDictionary.AddRepositoryType(dataType, RepositoryType);
    }

    private IHistoryTypesDictionary GetTypesDictionary()
    {
        if (Services.SingleOrDefault(service => service.ServiceType == typeof(IHistoryTypesDictionary))?.ImplementationInstance is IHistoryTypesDictionary registration)
        {
            return registration;
        }

        var newRegistration = new HistoryTypesDictionary();
        Services.AddSingleton<IHistoryTypesDictionary>(newRegistration);

        return newRegistration;
    }
}