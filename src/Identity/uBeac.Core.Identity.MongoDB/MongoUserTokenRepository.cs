using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB
{
    public class MongoUserTokenRepository<TUserKey> : MongoEntityRepository<TUserKey, UserToken<TUserKey>>, IUserTokenRepository<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public MongoUserTokenRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }

    public class MongoUserTokenRepository : MongoUserTokenRepository<Guid>, IUserTokenRepository
    {
        public MongoUserTokenRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
