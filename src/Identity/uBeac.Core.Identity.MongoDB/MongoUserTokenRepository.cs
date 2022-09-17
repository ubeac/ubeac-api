using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserTokenRepository<TUserKey, TContext> : MongoEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityEventManager<TUserKey, UserToken<TUserKey>> eventManager) : base(mongoDbContext, appContext, eventManager)
    {
    }
}

public class MongoUserTokenRepository<TContext> : MongoUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityEventManager<Guid, UserToken<Guid>> eventManager) : base(mongoDbContext, appContext, eventManager)
    {
    }
}