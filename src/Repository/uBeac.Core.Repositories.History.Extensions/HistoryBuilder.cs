
namespace Microsoft.Extensions.DependencyInjection;

public interface IHistoryBuilder
{
    IHistoryBuilder For<T>();
}

public class HistoryBuilder : IHistoryBuilder
{
    protected readonly IServiceCollection Services;

    protected readonly Type RepositoryType;
    protected readonly IHistoryTypesDictionary TypesDictionary;

    public HistoryBuilder(IServiceCollection services, Type repositoryType)
    {
        Services = services;
        RepositoryType = repositoryType;

        if (Services.SingleOrDefault(service => service.ServiceType == typeof(IHistoryTypesDictionary))?.ImplementationInstance is IHistoryTypesDictionary typesDictionary)
        {
            TypesDictionary = typesDictionary;
        }
        else
        {
            TypesDictionary = new HistoryTypesDictionary();
            Services.AddSingleton<IHistoryTypesDictionary>(TypesDictionary);
        }
    }

    public IHistoryBuilder For<T>()
    {
        TypesDictionary.AddRepositoryType(typeof(T), RepositoryType);
        return this;
    }
}