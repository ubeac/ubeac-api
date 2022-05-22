using Microsoft.Extensions.Configuration;
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
}