using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoRoleRepository<TRoleKey, TRole> : MongoEntityRepository<TRoleKey, TRole>, IRoleRepository<TRoleKey, TRole>
   where TRoleKey : IEquatable<TRoleKey>
   where TRole : Role<TRoleKey>
{
    public MongoRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TRole>.IndexKeys.Ascending(role => role.NormalizedName);
            var indexModel = new CreateIndexModel<TRole>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}

public class MongoRoleRepository<TRole> : MongoRoleRepository<Guid, TRole>, IRoleRepository<TRole> where TRole : Role
{
    public MongoRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

