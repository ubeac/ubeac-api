using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRoleRepository<TKey, TUnitRole> : MongoEntityRepository<TKey, TUnitRole>, IUnitRoleRepository<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
    public MongoUnitRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoUnitRoleRepository<TUnitRole> : MongoUnitRoleRepository<Guid, TUnitRole>, IUnitRoleRepository<TUnitRole>
    where TUnitRole : UnitRole
{
    public MongoUnitRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}