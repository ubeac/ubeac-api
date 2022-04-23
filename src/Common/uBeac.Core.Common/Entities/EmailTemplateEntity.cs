namespace uBeac;

public class EmailTemplateEntity<TKey> : AuditEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public string Category { get; set; }
    public string SubCategory { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }

    public string DefaultCc { get; set; }
    public string DefaultBcc { get; set; }
}

public class EmailTemplateEntity : EmailTemplateEntity<Guid>, IAuditEntity
{
}