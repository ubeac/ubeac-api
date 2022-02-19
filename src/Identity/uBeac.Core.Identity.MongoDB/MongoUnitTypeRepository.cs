using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitTypeRepository<TKey, TUnitType> : MongoEntityRepository<TKey, TUnitType>, IUnitTypeRepository<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{

    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
        // Create Indexes
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexKeys = Builders<TUnitType>.IndexKeys.Ascending(unitType => unitType.Code);
        var indexModel = new CreateIndexModel<TUnitType>(indexKeys, indexOptions);
        Collection.Indexes.CreateOne(indexModel);
    }
}

public class MongoUnitTypeRepository<TUnitType> : MongoUnitTypeRepository<Guid, TUnitType>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
{
    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}