namespace uBeac.Localization;

public class LocalizationService : ILocalizationService
{
    protected readonly ILocalizationRepository Repository;
    protected readonly ILocalizationCachingService CachingService;

    public LocalizationService(ILocalizationRepository repository, ILocalizationCachingService cachingService)
    {
        Repository = repository;
        CachingService = cachingService;
    }

    public async Task<IEnumerable<LocalizationValue>> GetAll(CancellationToken cancellationToken = default)
    {
        var cacheValues = CachingService.GetAll();
        if (cacheValues != null) return cacheValues;

        var values = await Repository.GetAll(cancellationToken);

        new Thread(() => CachingService.AddRange(values)).Start();

        return values;
    }

    public async Task<IEnumerable<LocalizationValue>> GetAllByCultureName(string cultureName, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken);

        return values.Where(x => x.CultureName == cultureName);
    }

    public async Task<bool> ExistsValue(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        var values = await GetAllByCultureName(cultureName, cancellationToken);

        return values.Any(x => x.Key == key);
    }

    public async Task<LocalizationValue> GetValue(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        var values = await GetAllByCultureName(cultureName, cancellationToken);

        return values.FirstOrDefault(x => x.Key == key);
    }

    public async Task Upsert(LocalizationValue entity, CancellationToken cancellationToken = default)
    {
        await Repository.Upsert(entity, cancellationToken);

        new Thread(() => CachingService.Clear()).Start();
    }

    public async Task Delete(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        await Repository.Delete(key, cultureName, cancellationToken);

        new Thread(() => CachingService.Clear()).Start();
    }
}
