using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using uBeac;
using uBeac.Web;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationContext<TApplicationContext>(this IServiceCollection services)
        where TApplicationContext : class, IApplicationContext
    {
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

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        var corsPolicy = configurationSection.Get<CorsPolicyOptions>();

        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy.Name, corsPolicy);
        });

        return services;
    }

    public static IServiceCollection AddHttpsPolicy(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        var hsts = configurationSection.Get<HstsOptions>();

        services.AddHsts(options =>
        {
            options.Preload = hsts.Preload;
            options.IncludeSubDomains = hsts.IncludeSubDomains;
            options.MaxAge = TimeSpan.FromDays(hsts.MaxAge);
            if (hsts.ExcludedHosts != null) foreach (var host in hsts.ExcludedHosts) options.ExcludedHosts.Add(host);
        });

        return services;
    }
}
