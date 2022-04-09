using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitTypeRepository<TKey, TUnitType, TContext> : MongoEntityRepository<TKey, TUnitType, TContext>, IUnitTypeRepository<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
    where TContext : IMongoDBContext
{

    public MongoUnitTypeRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityHistoryRepository<TKey, TUnitType> historyRepository) : base(mongoDbContext, appContext, historyRepository)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TUnitType>.IndexKeys.Ascending(unitType => unitType.Code);
            var indexModel = new CreateIndexModel<TUnitType>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}

public class MongoUnitTypeRepository<TUnitType, TContext> : MongoUnitTypeRepository<Guid, TUnitType, TContext>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
    where TContext : IMongoDBContext
{
    public MongoUnitTypeRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityHistoryRepository<TUnitType> historyRepository) : base(mongoDbContext, appContext, historyRepository)
    {
    }
}