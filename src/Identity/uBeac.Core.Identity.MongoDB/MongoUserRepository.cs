using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserRepository<TUserKey, TUser> : MongoEntityRepository<TUserKey, TUser>, IUserRepository<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    public MongoUserRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoUserRepository<TUser> : MongoEntityRepository<TUser>, IUserRepository<TUser>
    where TUser : User
{
    public MongoUserRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

