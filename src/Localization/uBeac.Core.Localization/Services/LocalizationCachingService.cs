using Microsoft.Extensions.Caching.Memory;

namespace uBeac.Localization;

public class InMemoryLocalizationCachingService : ILocalizationCachingService
{
    private readonly IMemoryCache _memoryCache;

    protected const string CachingKey = "uBeac:LocalizationValues";

    public InMemoryLocalizationCachingService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void AddRange(IEnumerable<LocalizationValue> values)
    {
        _memoryCache.Set(CachingKey, values);
    }

    public IEnumerable<LocalizationValue> GetAll()
    {
        var exist = _memoryCache.TryGetValue<IEnumerable<LocalizationValue>>(CachingKey, out var values);

        return exist ? values : null;
    }

    public void Clear()
    {
        _memoryCache.Remove(CachingKey);
    }
}
