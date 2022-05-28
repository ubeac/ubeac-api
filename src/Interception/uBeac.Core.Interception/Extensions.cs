using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace uBeac.Interception;

public static class Extensions
{
    public static IServiceCollection Intercept<T>(this IServiceCollection services)
    {
        var targetType = typeof(T);

        var descriptors = services.Where(service => service.ServiceType == targetType).ToList();
        
        services.TryAddScoped<IInterceptionHandler<T>, InterceptionHandler<T>>();

        foreach (var descriptor in descriptors)
        {
            var proxyDescriptor = ServiceDescriptor.Describe(targetType, serviceProvider =>
            {
                var instance = GetInstance<T>(serviceProvider, descriptor);
                var handler = GetHandler<T>(serviceProvider);

                return InterceptionDispatchProxy<T>.Create(instance, handler);
            }, descriptor.Lifetime);

            services.Replace(proxyDescriptor);
        }

        return services;
    }

    private static T GetInstance<T>(IServiceProvider serviceProvider, ServiceDescriptor targetServiceDescriptor)
    {
        if (targetServiceDescriptor.ImplementationInstance is T implementationInstance)
        {
            return implementationInstance;
        }

        if (targetServiceDescriptor.ImplementationFactory?.Invoke(serviceProvider) is T instance)
        {
            return instance;
        }

        if (targetServiceDescriptor.ImplementationType != null)
        {
            return (T)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, targetServiceDescriptor.ImplementationType);
        }

        throw new InvalidOperationException();
    }

    private static IInterceptionHandler<T> GetHandler<T>(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IInterceptionHandler<T>>();
    }
}