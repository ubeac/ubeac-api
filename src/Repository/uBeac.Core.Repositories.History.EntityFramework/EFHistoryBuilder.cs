using Microsoft.Extensions.DependencyInjection;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Repositories.History.EntityFramework;

public class EFHistoryBuilder<TContext> : IHistoryBuilder
    where TContext : EFDbContext
{
    public IServiceCollection Services { get; }

    public EFHistoryBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IHistoryBuilder For<T>() => this.AddRepository<T, EFEntityRepository<HistoryEntity<T>, TContext>>(Services);
}