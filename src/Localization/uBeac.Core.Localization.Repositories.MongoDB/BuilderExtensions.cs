using uBeac.Localization.Repositories.MongoDB;
using uBeac.Repositories.MongoDB;

namespace uBeac.Localization;

public static class BuilderExtensions
{
    public static ILocalizationBuilder UseMongoDB<TContext>(this ILocalizationBuilder builder) where TContext : IMongoDBContext
    {
        builder.SetRepository(typeof(MongoDBLocalizationRepository<TContext>));

        return builder;
    }
}
