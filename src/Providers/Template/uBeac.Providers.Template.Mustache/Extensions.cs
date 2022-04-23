using uBeac.Providers.Template;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMustacheTemplateRendering(this IServiceCollection services)
    {
        services.AddScoped<ITemplateRenderingProvider, MustacheTemplateRenderingProvider>();

        return services;
    }
}