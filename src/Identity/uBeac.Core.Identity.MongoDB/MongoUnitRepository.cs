using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRepository<TKey, TUnit> : MongoEntityRepository<TKey, TUnit>, IUnitRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TUnit>.IndexKeys.Combine(new List<IndexKeysDefinition<TUnit>>
            {
                Builders<TUnit>.IndexKeys.Ascending(unit => unit.Type),
                Builders<TUnit>.IndexKeys.Ascending(unit => unit.Code)
            });
            var indexModel = new CreateIndexModel<TUnit>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}

public class MongoUnitRepository<TUnit> : MongoUnitRepository<Guid, TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
