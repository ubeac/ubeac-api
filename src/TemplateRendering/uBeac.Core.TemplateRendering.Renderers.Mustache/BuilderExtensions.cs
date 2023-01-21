using uBeac.TemplateRendering;
using uBeac.TemplateRendering.Renderers.Mustache;

namespace Microsoft.Extensions.DependencyInjection;

public static class BuilderExtensions
{
    public static ITemplateRenderingBuilder UseMustacheRenderer(this ITemplateRenderingBuilder builder)
    {
        builder.SetRenderer(typeof(MustacheTemplateRenderer));

        return builder;
    }
}
