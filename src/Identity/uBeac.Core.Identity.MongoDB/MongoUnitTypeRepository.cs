using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitTypeRepository<TKey, TUnitType> : MongoEntityRepository<TKey, TUnitType>, IUnitTypeRepository<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{

    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoUnitTypeRepository<TUnitType> : MongoUnitTypeRepository<Guid, TUnitType>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
{
    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}