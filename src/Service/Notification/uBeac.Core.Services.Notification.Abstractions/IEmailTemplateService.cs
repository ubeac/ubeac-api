namespace uBeac.Services;

public interface IEmailTemplateService<TKey, TEmailTemplate> : IEntityService<TKey, TEmailTemplate>
    where TKey : IEquatable<TKey>
    where TEmailTemplate : EmailTemplateEntity<TKey>
{
    Task<TEmailTemplate> Find(string category, string subCategory, CancellationToken cancellationToken = default);
}

public interface IEmailTemplateService<TEmailTemplate> : IEmailTemplateService<Guid, TEmailTemplate>, IEntityService<TEmailTemplate>
    where TEmailTemplate : EmailTemplateEntity
{
}

public interface IEmailTemplateService : IEmailTemplateService<EmailTemplateEntity>
{
}