using MongoDB.Driver;
using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserRepository<TUserKey, TUser, TContext> : MongoEntityRepository<TUserKey, TUser, TContext>, IUserRepository<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
    where TContext : IMongoDBContext
{
    public MongoUserRepository(TContext mongoDbContext, IApplicationContext appContext, IHistoryManager history) : base(mongoDbContext, appContext, history)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TUser>.IndexKeys.Ascending(user => user.NormalizedUserName);
            var indexModel = new CreateIndexModel<TUser>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}

public class MongoUserRepository<TUser, TContext> : MongoUserRepository<Guid, TUser, TContext>, IUserRepository<TUser>
    where TUser : User
    where TContext : IMongoDBContext
{
    public MongoUserRepository(TContext mongoDbContext, IApplicationContext appContext, IHistoryManager history) : base(mongoDbContext, appContext, history)
    {
    }
}