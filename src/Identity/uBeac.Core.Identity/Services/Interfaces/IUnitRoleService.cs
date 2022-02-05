using uBeac.Services;

namespace uBeac.Identity;

public interface IUnitRoleService<TKey, TUnitRole> : IEntityService<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
}

public interface IUnitRoleService<TUnitRole> : IUnitRoleService<Guid, TUnitRole>
    where TUnitRole : UnitRole
{
}