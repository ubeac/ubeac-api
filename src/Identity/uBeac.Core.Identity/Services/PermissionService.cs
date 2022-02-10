using uBeac.Services;

namespace uBeac.Identity;

public class PermissionService<TKey, TPermission> : EntityService<TKey, TPermission>, IPermissionService<TKey, TPermission>
    where TKey : IEquatable<TKey>
    where TPermission : Permission<TKey>
{
    public PermissionService(IPermissionRepository<TKey, TPermission> repository) : base(repository)
    {
    }
}

public class PermissionService<TPermission> : PermissionService<Guid, TPermission>, IPermissionService<TPermission>
    where TPermission : Permission
{
    public PermissionService(IPermissionRepository<TPermission> repository) : base(repository)
    {
    }
}