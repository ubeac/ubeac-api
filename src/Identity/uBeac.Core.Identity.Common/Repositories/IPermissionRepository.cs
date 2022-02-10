using uBeac.Repositories;

namespace uBeac.Identity;

public interface IPermissionRepository<TKey, TPermission> : IEntityRepository<TKey, TPermission>
    where TKey : IEquatable<TKey>
    where TPermission : Permission<TKey>
{
}

public interface IPermissionRepository<TPermission> : IPermissionRepository<Guid, TPermission>, IEntityRepository<TPermission>
    where TPermission : Permission 
{
}