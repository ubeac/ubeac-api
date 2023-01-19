using MongoDB.Driver;
using MongoDB.Driver.Linq;
using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.Localization.Repositories.MongoDB;

public interface IMongoDBLocalizationRepository : ILocalizationRepository
{
}

public class MongoDBLocalizationRepository<TContext> : MongoEntityRepository<LocalizationValue, TContext>, IMongoDBLocalizationRepository
    where TContext : IMongoDBContext
{
    public MongoDBLocalizationRepository(TContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task Upsert(LocalizationValue entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = await GetByKey(entity.Key, entity.CultureName, cancellationToken);

        if (dbEntity == null)
        {
            await Create(entity, cancellationToken);
            return;
        }

        dbEntity.Value = entity.Value;
        await Update(entity, cancellationToken);
    }

    public async Task Delete(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        var entity = await GetByKey(key, cultureName, cancellationToken);
        await Delete(entity.Id, cancellationToken);
    }

    public async Task<LocalizationValue> GetByKey(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        return await Collection.AsQueryable()
            .FirstOrDefaultAsync(x => x.Key == key && x.CultureName == cultureName, cancellationToken);
    }
}
