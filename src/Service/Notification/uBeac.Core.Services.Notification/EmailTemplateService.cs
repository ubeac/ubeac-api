using uBeac.Repositories;

namespace uBeac.Services;

public class EmailTemplateService<TKey, TEmailTemplate> : EntityService<TKey, TEmailTemplate>, IEmailTemplateService<TKey, TEmailTemplate>
    where TKey : IEquatable<TKey>
    where TEmailTemplate : EmailTemplateEntity<TKey>, new()
{
    public EmailTemplateService(IEmailTemplateRepository<TKey, TEmailTemplate> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }

    public async Task<TEmailTemplate> Find(string category, string subCategory, CancellationToken cancellationToken = default)
    {
        var items = await Repository.Find(x => x.Category == category && x.SubCategory == subCategory, cancellationToken);

        if (items == null) return new TEmailTemplate
        {
            Body = $"Email template for category: {category}, sub-category: {subCategory} not found!",
            Category = category,
            SubCategory = subCategory,
            Subject = "Email template not found!"
        };

        return items.First();
    }
}

public class EmailTemplateService<TEmailTemplate> : EmailTemplateService<Guid, TEmailTemplate>, IEmailTemplateService<TEmailTemplate>
    where TEmailTemplate : EmailTemplateEntity, new()
{
    public EmailTemplateService(IEmailTemplateRepository<TEmailTemplate> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}

public class EmailTemplateService : EmailTemplateService<EmailTemplateEntity>, IEmailTemplateService
{
    public EmailTemplateService(IEmailTemplateRepository repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}