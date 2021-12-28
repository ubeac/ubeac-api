using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB
{
    public class MongoRoleRepository<TRoleKey, TRole> : MongoEntityRepository<TRoleKey, TRole>, IRoleRepository<TRoleKey, TRole>
       where TRoleKey : IEquatable<TRoleKey>
       where TRole : Role<TRoleKey>
    {
        public MongoRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
