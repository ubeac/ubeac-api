using uBeac.TemplateRendering;
using uBeac.TemplateRendering.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTemplateRendering(this IServiceCollection services, Action<ITemplateRenderingBuilder> builder)
    {
        var serviceCollectionBuilder = new ServiceCollectionTemplateRenderingBuilder(services);
        
        builder(serviceCollectionBuilder);

        serviceCollectionBuilder.SetService(typeof(ContentTemplateService));

        return services;
    } 
}
