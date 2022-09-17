using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Repositories;
using uBeac.Repositories.Events;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityEventHandling(this IServiceCollection services)
    {
        // Adding default event handlers
        services.AddScoped<IGeneralEntityEventHandler, SetAuditPropertiesEventHandler>();

        // Adding event manager
        services.AddScoped(typeof(IEntityEventManager<>), typeof(EntityEventManager<>));
        services.AddScoped(typeof(IEntityEventManager<,>), typeof(EntityEventManager<,>));

        return services;
    }

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