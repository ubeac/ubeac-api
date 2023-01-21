using uBeac.Repositories.MongoDB;
using uBeac.TemplateRendering;
using uBeac.TemplateRendering.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class BuilderExtensions
{
    public static ITemplateRenderingBuilder UseMongoDB<TContext>(this ITemplateRenderingBuilder builder)
        where TContext : IMongoDBContext
    {
        builder.SetRepository(typeof(MongoDBContentTemplateRepository<TContext>));

        return builder;
    }
}
