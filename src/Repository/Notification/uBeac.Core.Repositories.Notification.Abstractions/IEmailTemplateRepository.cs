namespace uBeac.Repositories;

public interface IEmailTemplateRepository<TKey, TEmailTemplate> : IEntityRepository<TKey, TEmailTemplate>
    where TKey : IEquatable<TKey>
    where TEmailTemplate : EmailTemplateEntity<TKey>
{
}

public interface IEmailTemplateRepository<TEmailTemplate> : IEmailTemplateRepository<Guid, TEmailTemplate>, IEntityRepository<TEmailTemplate>
    where TEmailTemplate : EmailTemplateEntity
{
}

public interface IEmailTemplateRepository : IEmailTemplateRepository<EmailTemplateEntity>
{
}