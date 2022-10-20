
using uBeac.Web.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHttpLogServices(this IServiceCollection services)           
        {
            services.AddScoped<IHttpLogChanges, HttpLogChanges>();
            return services;
        }
    }
}
