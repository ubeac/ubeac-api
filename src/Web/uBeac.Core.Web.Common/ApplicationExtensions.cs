using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uBeac;
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

    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        var seeders = app.ApplicationServices.CreateScope()
            .ServiceProvider
            .GetServices<IDataSeeder>();
        
        foreach (var seeder in seeders)
        {
            seeder.SeedAsync().Wait();
        }

        return app;
    }
}
