using uBeac.Services;

namespace uBeac.Localization;

public interface ILocalizationService : IService
{
    Task<IEnumerable<LocalizationValue>> GetAllByCultureName(string cultureName, CancellationToken cancellationToken = default);

    Task<bool> ExistsValue(string key, string cultureName, CancellationToken cancellationToken = default);

    Task<LocalizationValue> GetValue(string key, string cultureName, CancellationToken cancellationToken = default);

    Task Upsert(LocalizationValue entity, CancellationToken cancellationToken = default);

    Task Delete(string key, string cultureName, CancellationToken cancellationToken = default);
}
