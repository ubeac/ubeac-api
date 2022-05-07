using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRoleRepository<TKey, TUnitRole, TContext> : MongoEntityRepository<TKey, TUnitRole, TContext>, IUnitRoleRepository<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
    where TContext : IMongoDBContext
{
    public MongoUnitRoleRepository(TContext mongoDbContext, IApplicationContext appContext, HistoryFactory historyFactory) : base(mongoDbContext, appContext, historyFactory)
    {
    }
}

public class MongoUnitRoleRepository<TUnitRole, TContext> : MongoUnitRoleRepository<Guid, TUnitRole, TContext>, IUnitRoleRepository<TUnitRole>
    where TUnitRole : UnitRole
    where TContext : IMongoDBContext
{
    public MongoUnitRoleRepository(TContext mongoDbContext, IApplicationContext appContext, HistoryFactory historyFactory) : base(mongoDbContext, appContext, historyFactory)
    {
    }
}