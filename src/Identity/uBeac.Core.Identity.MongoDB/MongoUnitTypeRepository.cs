using MongoDB.Bson;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitTypeRepository<TKey, TUnitType> : MongoEntityRepository<TKey, TUnitType>, IUnitTypeRepository<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{

    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
        // TODO: clean/move this to an startupo/singleton area
        var indexName = GetCollectionName() + "_TUnitType_UnitCode";
        var index = new BsonDocument
        {
            { indexName, 1 }
        };

        var indexModel = new CreateIndexModel<TUnitType>(index, new CreateIndexOptions { Unique = true });
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