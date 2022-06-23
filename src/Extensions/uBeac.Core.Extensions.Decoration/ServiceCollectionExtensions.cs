using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Extensions.Decoration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDecorator<TTargetInterface, TTargetImplementation, TDecorator>(this IServiceCollection services)
        where TTargetImplementation : class, TTargetInterface
        where TDecorator : class
    {
        return services.Replace(new ServiceDescriptor(typeof(TTargetInterface), serviceProvider =>
        {
            var target = ActivatorUtilities.GetServiceOrCreateInstance<TTargetImplementation>(serviceProvider);
            var decorator = ActivatorUtilities.CreateInstance<TDecorator>(serviceProvider, target);
            return DecorationProxy<TTargetInterface, TDecorator>.Decorate(target, decorator);
        }, ServiceLifetime.Scoped));
    }
}