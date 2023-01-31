using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace uBeac.Localization;

public class ServiceCollectionLocalizationBuilder : ILocalizationBuilder
{
    protected readonly IServiceCollection Services;

    public ServiceCollectionLocalizationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public ILocalizationBuilder SetRepository(Type repositoryType)
    {
        Services.TryAddScoped(typeof(ILocalizationRepository), repositoryType);

        return this;
    }

    public ILocalizationBuilder SetService(Type serviceType)
    {
        Services.TryAddScoped(typeof(ILocalizationService), serviceType);

        return this;
    }

    public ILocalizationBuilder SetCachingService(Type cachingServiceType)
    {
        Services.TryAddScoped(typeof(ILocalizationCachingService), cachingServiceType);

        return this;
    }
}
