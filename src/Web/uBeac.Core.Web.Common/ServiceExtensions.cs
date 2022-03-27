using AutoMapper;
using uBeac;
using uBeac.Web;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMappingProfile<T>(this IServiceCollection services) where T: Profile
        {
            services.AddAutoMapper(typeof(T));
            return services;
        }

        public static IServiceCollection AddDebugger<TDebugger>(this IServiceCollection services) where TDebugger : class, IDebugger
        {
            services.AddScoped<IDebugger, TDebugger>();
            return services;
        }

        public static IServiceCollection AddDebugger(this IServiceCollection services)
        {
            return AddDebugger<Debugger>(services);
        }
    }
}