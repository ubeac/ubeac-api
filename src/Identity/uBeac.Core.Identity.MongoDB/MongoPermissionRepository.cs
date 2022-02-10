using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoPermissionRepository<TKey, TPermission> : MongoEntityRepository<TKey, TPermission>, IPermissionRepository<TKey, TPermission>
    where TKey : IEquatable<TKey>
    where TPermission : Permission<TKey>
{
    public MongoPermissionRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}

public class MongoPermissionRepository<TPermission> : MongoPermissionRepository<Guid, TPermission>, IPermissionRepository<TPermission>
    where TPermission : Permission
{
    public MongoPermissionRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}