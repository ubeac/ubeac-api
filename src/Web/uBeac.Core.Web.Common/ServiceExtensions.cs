using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMappingProfile<T>(this IServiceCollection services) where T: Profile
        {
            services.AddAutoMapper(typeof(T));
            return services;
        }
    }
}