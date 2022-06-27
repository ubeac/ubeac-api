using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserTokenRepository<TUserKey, TContext> : MongoEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IHistoryManager history) : base(mongoDbContext, appContext, history)
    {
    }
}

public class MongoUserTokenRepository<TContext> : MongoUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : IMongoDBContext
{
    public MongoUserTokenRepository(TContext mongoDbContext, IApplicationContext appContext, IHistoryManager history) : base(mongoDbContext, appContext, history)
    {
    }
}

