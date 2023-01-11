using uBeac.Services;

namespace uBeac.TemplateRendering.Services;

public class ContentTemplateService : EntityService<ContentTemplate>, IContentTemplateService
{
    protected new readonly IContentTemplateRepository Repository;
    protected readonly ITemplateRenderer Renderer;

    public ContentTemplateService(IContentTemplateRepository repository, ITemplateRenderer renderer) : base(repository)
    {
        Repository = repository;
        Renderer = renderer;
    }

    public async Task<ContentTemplate> GetByUniqueKey(string uniqueKey, CancellationToken cancellationToken = default)
    {
        return await Repository.GetByUniqueKey(uniqueKey, cancellationToken);
    }

    public async Task<RenderedContent> Render(string templateKey, object model, CancellationToken cancellationToken = default)
    {
        var template = await Repository.GetByUniqueKey(templateKey, cancellationToken);

        return await Render(template, model, cancellationToken);
    }

    public async Task<RenderedContent> Render(ContentTemplate template, object model, CancellationToken cancellationToken = default)
    {
        var result = new RenderedContent();

        if (!string.IsNullOrWhiteSpace(template.Subject))
        {
            result.Subject = await Renderer.Render(template.Subject, model);
        }

        result.Body = await Renderer.Render(template.Body, model);

        return result;
    }
}
