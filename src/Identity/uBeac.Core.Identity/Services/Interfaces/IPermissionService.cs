using uBeac.Services;

namespace uBeac.Identity;

public interface IPermissionService<TKey, TPermission> : IEntityService<TKey, TPermission>
    where TKey : IEquatable<TKey>
    where TPermission : Permission<TKey>
{
}

public interface IPermissionService<TPermission> : IPermissionService<Guid, TPermission>
    where TPermission : Permission
{
}