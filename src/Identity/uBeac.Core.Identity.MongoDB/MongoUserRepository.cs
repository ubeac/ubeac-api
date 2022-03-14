﻿using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUserRepository<TUserKey, TUser> : MongoAuditEntityRepository<TUserKey, TUser>, IUserRepository<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    public MongoUserRepository(IApplicationContext appContext, IMongoDBContext mongoDbContext) : base(appContext, mongoDbContext)
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

public class MongoUserRepository<TUser> : MongoUserRepository<Guid, TUser>, IUserRepository<TUser>
    where TUser : User
{
    public MongoUserRepository(IApplicationContext appContext, IMongoDBContext mongoDbContext) : base(appContext, mongoDbContext)
    {
    }
}