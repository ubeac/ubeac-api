namespace uBeac.Providers.Template;

public interface ITemplateRenderingProvider
{
    Task<string> Render(string templateContent, object model, CancellationToken cancellationToken = default);
}