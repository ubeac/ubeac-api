using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB
{
    public class MongoUserTokenReporitory<TUserKey> : MongoEntityRepository<TUserKey, UserToken<TUserKey>>, IUserTokenRepository<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public MongoUserTokenReporitory(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }

    public class MongoUserTokenReporitory : MongoUserTokenReporitory<Guid>, IUserTokenRepository
    {
        public MongoUserTokenReporitory(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
