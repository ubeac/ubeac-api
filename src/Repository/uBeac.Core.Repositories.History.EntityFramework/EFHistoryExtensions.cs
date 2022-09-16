using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;
using uBeac.Repositories.History.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class EFHistoryExtensions
{
    public static IHistoryBuilder AddEntityFrameworkHistory<TContext>(this IServiceCollection services)
        where TContext : EFDbContext 
        => new EFHistoryBuilder<TContext>(services);
}