namespace uBeac.TemplateRendering;

public interface ITemplateRenderingBuilder
{
    ITemplateRenderingBuilder SetRepository(Type repositoryType);
    ITemplateRenderingBuilder SetService(Type serviceType);
    ITemplateRenderingBuilder SetRenderer(Type rendererType);
}
