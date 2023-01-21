using MongoDB.Driver;
using MongoDB.Driver.Linq;
using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.TemplateRendering.Repositories.MongoDB;

public interface IMongoDBContentTemplateRepository : IContentTemplateRepository
{
}

public class MongoDBContentTemplateRepository<TContext> : MongoEntityRepository<ContentTemplate, TContext>, IMongoDBContentTemplateRepository
    where TContext : IMongoDBContext
{
    public MongoDBContentTemplateRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<ContentTemplate> GetByUniqueKey(string uniqueKey, CancellationToken cancellationToken = default)
    {
        return await Collection
            .AsQueryable()
            .SingleAsync(x => x.UniqueKey == uniqueKey, cancellationToken);
    }
}
