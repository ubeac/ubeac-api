using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRepository<TKey, TUnit> : MongoEntityRepository<TKey, TUnit>, IUnitRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoUnitRepository<TUnit> : MongoUnitRepository<Guid, TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
