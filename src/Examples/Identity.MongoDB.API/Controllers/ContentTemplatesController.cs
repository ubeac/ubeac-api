using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using uBeac.TemplateRendering;
using uBeac.Web;
using IResult = uBeac.IResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Identity.MongoDB.API.Controllers;

public class ContentTemplatesController : BaseController
{
    protected readonly IContentTemplateService ContentTemplateService;

    public ContentTemplatesController(IContentTemplateService contentTemplateService)
    {
        ContentTemplateService = contentTemplateService;
    }

    [HttpPost]
    public async Task<IResult> Test(string siteName = "uBeac", string userName = "Hesam", CancellationToken cancellationToken = default)
    {
        var randomNumber = new Random().Next();

        var templateKey = $"sign-up-email-{randomNumber}";

        var template = new ContentTemplate
        {
            UniqueKey = templateKey,
            Subject = "Welcome to {{SiteName}}",
            Body = "Hello {{UserName}}, Your account has been created successfully!"
        };

        await ContentTemplateService.Create(template, cancellationToken);

        var model = new SignUpContent { SiteName = siteName, UserName = userName };
        var result = await ContentTemplateService.Render(templateKey, model, cancellationToken);

        return result.ToResult();
    }

    private class SignUpContent
    {
        public string SiteName { get; set; }
        public string UserName { get; set; }
    }
}
