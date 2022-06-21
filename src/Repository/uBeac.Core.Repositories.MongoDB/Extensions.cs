using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : IRepository
        where TImplementation : class, IRepository
    {
        services.TryAddScoped(typeof(TInterface), typeof(TImplementation));
        return services;

    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type interfaceType, Type implementationType)
    {
        services.TryAddScoped(interfaceType, implementationType);
        return services;
    }
}
