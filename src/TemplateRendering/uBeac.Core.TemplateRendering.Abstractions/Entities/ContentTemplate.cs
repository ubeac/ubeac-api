namespace uBeac.TemplateRendering;

public class ContentTemplate : Entity
{
    public string UniqueKey { get; set; } // "sign-up-email"
    public string Subject { get; set; } // "Welcome to {{SiteName}}"
    public string Body { get; set; } // "Hello {{Name}}, Your account has been created successfully!"
}
