using Microsoft.Extensions.Localization;
using uBeac.Localization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services, Action<ILocalizationBuilder> builder)
    {
        var serviceCollectionBuilder = new ServiceCollectionLocalizationBuilder(services);

        builder(serviceCollectionBuilder);

        serviceCollectionBuilder.SetService(typeof(LocalizationService));

        services.AddScoped<IStringLocalizer, Localizer>();

        return services;
    }

    public static ILocalizationBuilder UseInMemoryCaching(this ILocalizationBuilder services)
    {
        services.SetCachingService(typeof(InMemoryLocalizationCachingService));

        return services;
    }
}
