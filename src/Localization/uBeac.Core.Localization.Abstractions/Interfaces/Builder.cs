namespace uBeac.Localization;

public interface ILocalizationBuilder
{
    ILocalizationBuilder SetRepository(Type repositoryType);
    ILocalizationBuilder SetService(Type serviceType);
    ILocalizationBuilder SetCachingService(Type cachingServiceType);
}
