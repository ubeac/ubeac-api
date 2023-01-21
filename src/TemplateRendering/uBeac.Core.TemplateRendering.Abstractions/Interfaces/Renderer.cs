namespace uBeac.TemplateRendering;

public interface ITemplateRenderer
{
    Task<string> Render(string template, object model);
}
