using Stubble.Core.Builders;
using Stubble.Extensions.JsonNet;

namespace uBeac.TemplateRendering.Renderers.Mustache;

public class MustacheTemplateRenderer : ITemplateRenderer
{
    public async Task<string> Render(string template, object model)
    {
        var stubble = new StubbleBuilder()
            .Configure(settings =>
            {
                settings.AddJsonNet();
                settings.SetIgnoreCaseOnKeyLookup(true);
                settings.SetMaxRecursionDepth(512);
            })
            .Build();

        return await stubble.RenderAsync(template, model);
    }
}
