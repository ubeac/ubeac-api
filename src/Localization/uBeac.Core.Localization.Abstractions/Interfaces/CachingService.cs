using uBeac.Services;

namespace uBeac.Localization;

public interface ILocalizationCachingService : IService
{
    void AddRange(IEnumerable<LocalizationValue> values);

    IEnumerable<LocalizationValue> GetAll();

    void Clear();
}
