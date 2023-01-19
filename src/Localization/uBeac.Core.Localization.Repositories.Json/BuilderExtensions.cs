using uBeac.Localization.Repositories.Json;

namespace uBeac.Localization;

public static class BuilderExtensions
{
    public static ILocalizationBuilder UseJsonFiles(this ILocalizationBuilder builder)
    {
        builder.SetRepository(typeof(JsonLocalizationRepository));

        return builder;
    }
}
