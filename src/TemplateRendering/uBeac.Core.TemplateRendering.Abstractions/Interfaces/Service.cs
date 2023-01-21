using uBeac.Services;

namespace uBeac.TemplateRendering;

public interface IContentTemplateService : IEntityService<ContentTemplate>
{
    Task<ContentTemplate> GetByUniqueKey(string uniqueKey, CancellationToken cancellationToken = default);

    Task<RenderedContent> Render(string templateKey, object model, CancellationToken cancellationToken = default);
    Task<RenderedContent> Render(ContentTemplate template, object model, CancellationToken cancellationToken = default);
}
