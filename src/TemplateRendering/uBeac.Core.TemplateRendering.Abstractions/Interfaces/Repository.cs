using uBeac.Repositories;

namespace uBeac.TemplateRendering;

public interface IContentTemplateRepository : IEntityRepository<ContentTemplate>
{
    Task<ContentTemplate> GetByUniqueKey(string uniqueKey, CancellationToken cancellationToken = default);
}
