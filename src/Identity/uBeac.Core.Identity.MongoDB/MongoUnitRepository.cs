using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB;

public class MongoUnitRepository<TUnitKey, TUnit> : MongoEntityRepository<TUnitKey, TUnit>, IUnitRepository<TUnitKey, TUnit> 
    where TUnitKey : IEquatable<TUnitKey> 
    where TUnit : Unit<TUnitKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoUnitRepository<TUnit> : MongoEntityRepository<TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
