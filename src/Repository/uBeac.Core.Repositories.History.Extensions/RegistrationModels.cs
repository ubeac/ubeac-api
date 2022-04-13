using uBeac.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public interface IHistoryRegistration
{
    IServiceCollection Services { get; }
}

public class HistoryRegistration : IHistoryRegistration
{
    public HistoryRegistration(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }
}

public interface IHistoryRepositoryRegistration
{
    IHistoryRegistration HistoryRegistration { get; }
    Type RepositoryType { get; }
}

public class HistoryRepositoryRegistration<TRepository> : IHistoryRepositoryRegistration
    where TRepository : class, IHistoryRepository
{
    public HistoryRepositoryRegistration(IHistoryRegistration historyRegistration)
    {
        HistoryRegistration = historyRegistration;
    }

    public IHistoryRegistration HistoryRegistration { get; }
    public Type RepositoryType { get; } = typeof(TRepository);
}