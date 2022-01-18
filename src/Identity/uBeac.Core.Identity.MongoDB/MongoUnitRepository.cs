using MongoDB.Driver;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRepository<TUnitKey, TUnit> : MongoEntityRepository<TUnitKey, TUnit>, IUnitRepository<TUnitKey, TUnit> 
    where TUnitKey : IEquatable<TUnitKey> 
    where TUnit : Unit<TUnitKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public async Task<bool> Any(string code, string type, CancellationToken cancellationToken = default)
    {
        var findResult = await Collection.FindAsync(
            new ExpressionFilterDefinition<TUnit>(e => e.Code == code && e.Type == type),
            cancellationToken: cancellationToken);
        return findResult.ToEnumerable(cancellationToken).Any();
    }
}

public class MongoUnitRepository<TUnit> : MongoUnitRepository<Guid, TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
