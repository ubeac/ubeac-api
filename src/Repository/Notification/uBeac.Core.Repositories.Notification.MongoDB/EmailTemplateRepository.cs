namespace uBeac.Repositories.MongoDB;

public class MongoEmailTemplateRepository<TKey, TEmailTemplate, TContext> : MongoEntityRepository<TKey, TEmailTemplate, TContext>, IEmailTemplateRepository<TKey, TEmailTemplate>
    where TKey : IEquatable<TKey>
    where TEmailTemplate : EmailTemplateEntity<TKey>
    where TContext : IMongoDBContext
{
    public MongoEmailTemplateRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}


public class MongoEmailTemplateRepository<TEmailTemplate, TContext> : MongoEntityRepository<TEmailTemplate, TContext>, IEmailTemplateRepository<TEmailTemplate>
    where TEmailTemplate : EmailTemplateEntity
    where TContext : IMongoDBContext
{
    public MongoEmailTemplateRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}

public class MongoEmailTemplateRepository<TContext> : MongoEntityRepository<EmailTemplateEntity, TContext>, IEmailTemplateRepository
    where TContext : IMongoDBContext
{
    public MongoEmailTemplateRepository(TContext mongoDbContext, IApplicationContext appContext) : base(mongoDbContext, appContext)
    {
    }
}