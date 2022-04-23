using Stubble.Core.Builders;

namespace uBeac.Providers.Template;

public class MustacheTemplateRenderingProvider : ITemplateRenderingProvider
{
    public async Task<string> Render(string templateContent, object model, CancellationToken cancellationToken = default)
        => await new StubbleBuilder().Configure(settings =>
        {
            settings.SetIgnoreCaseOnKeyLookup(true);
            settings.SetMaxRecursionDepth(512);
        }).Build().RenderAsync(templateContent, model);
}