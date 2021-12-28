using uBeac.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterBaseServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));

            return services;
        }
    }
}
