using System.Reflection;
using AutoMapper;
using uBeac;
using uBeac.Web;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationContext<TApplicationContext>(this IServiceCollection services)
        where TApplicationContext : class, IApplicationContext
    {
        GlobalApplicationContext.ApplicationContextType = typeof(TApplicationContext);
        services.AddScoped<IApplicationContext, TApplicationContext>();
        return services;
    }

    public static IServiceCollection AddApplicationContext(this IServiceCollection services)
    {
        return AddApplicationContext<ApplicationContext>(services);
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
