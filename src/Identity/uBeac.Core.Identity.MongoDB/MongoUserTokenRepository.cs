using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserTokenRepository<TUserKey, TContext> : MongoEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityHistoryRepository<TUserKey, UserToken<TUserKey>> historyRepository) : base(mongoDbContext, appContext, historyRepository)
    {
    }
}

public class MongoUserTokenRepository<TContext> : MongoUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IEntityHistoryRepository<Guid, UserToken<Guid>> historyRepository) : base(mongoDbContext, appContext, historyRepository)
    {
    }
}

