using uBeac.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService<TInterface, TImplementation>(this IServiceCollection services)
           where TInterface : IService
           where TImplementation : class, IService
        {
            services.AddScoped(typeof(TInterface), typeof(TImplementation));
            return services;

        }
        public static IServiceCollection AddService(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            services.AddScoped(interfaceType, implementationType);
            return services;

        }
    }
}
