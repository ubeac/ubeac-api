using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace uBeac.TemplateRendering;

public class ServiceCollectionTemplateRenderingBuilder : ITemplateRenderingBuilder
{
    protected readonly IServiceCollection Services;

    public ServiceCollectionTemplateRenderingBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public ITemplateRenderingBuilder SetRepository(Type repositoryType)
    {
        Services.TryAddScoped(typeof(IContentTemplateRepository), repositoryType);

        return this;
    }

    public ITemplateRenderingBuilder SetService(Type serviceType)
    {
        Services.TryAddScoped(typeof(IContentTemplateService), serviceType);

        return this;
    }

    public ITemplateRenderingBuilder SetRenderer(Type rendererType)
    {
        Services.TryAddScoped(typeof(ITemplateRenderer), rendererType);

        return this;
    }
}
