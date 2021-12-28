using uBeac.Repositories;

namespace uBeac.Identity
{
    public interface IRoleRepository<TKey, TRole> : IEntityRepository<TKey, TRole>
       where TKey : IEquatable<TKey>
       where TRole : Role<TKey>
    {
    }
}
