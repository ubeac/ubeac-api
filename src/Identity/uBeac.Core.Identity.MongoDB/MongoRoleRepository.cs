using MongoDB.Driver;
using uBeac.Repositories.History;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoRoleRepository<TRoleKey, TRole, TContext> : MongoEntityRepository<TRoleKey, TRole, TContext>, IRoleRepository<TRoleKey, TRole>
   where TRoleKey : IEquatable<TRoleKey>
   where TRole : Role<TRoleKey>
   where TContext : IMongoDBContext
{
    public MongoRoleRepository(TContext mongoDbContext, IApplicationContext appContext, HistoryFactory historyFactory) : base(mongoDbContext, appContext, historyFactory)
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

public class MongoRoleRepository<TRole, TContext> : MongoRoleRepository<Guid, TRole, TContext>, IRoleRepository<TRole>
    where TRole : Role
    where TContext : IMongoDBContext
{
    public MongoRoleRepository(TContext mongoDbContext, IApplicationContext appContext, HistoryFactory historyFactory) : base(mongoDbContext, appContext, historyFactory)
    {
    }
}

