using uBeac.Services;

namespace uBeac.Identity;

public interface IUnitRoleService<TKey, TUnitRole> : IEntityService<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
    Task Insert(InsertUnitRole unitRole, CancellationToken cancellationToken = default);
    Task Replace(ReplaceUnitRole<TKey> unitRole, CancellationToken cancellationToken = default);
}

public interface IUnitRoleService<TUnitRole> : IUnitRoleService<Guid, TUnitRole>
    where TUnitRole : UnitRole
{
}