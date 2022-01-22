using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB;

public class MongoUnitRoleRepository<TUnitRoleKey, TUnitRole> : MongoEntityRepository<TUnitRoleKey, TUnitRole>, IUnitRoleRepository<TUnitRoleKey, TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
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