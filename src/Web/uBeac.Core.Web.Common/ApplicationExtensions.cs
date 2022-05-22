using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using uBeac.Web;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationExtensions
{
    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app, IConfigurationSection configurationSection)
    {
        var corsPolicy = configurationSection.Get<CorsPolicyOptions>();

        app.UseCors(corsPolicy.Name);

        return app;
    }

    public static IApplicationBuilder UseHstsOnProduction(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsProduction()) app.UseHsts();

        return app;
    }
}