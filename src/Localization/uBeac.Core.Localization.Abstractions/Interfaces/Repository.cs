using uBeac.Repositories;

namespace uBeac.Localization;

public interface ILocalizationRepository : IRepository
{
    Task<IEnumerable<LocalizationValue>> GetAll(CancellationToken cancellationToken = default);

    Task Upsert(LocalizationValue entity, CancellationToken cancellationToken = default);

    Task Delete(string key, string cultureName, CancellationToken cancellationToken = default);
}
