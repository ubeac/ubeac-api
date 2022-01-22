namespace uBeac.Identity;

public interface IUnitRoleService<TUnitRoleKey, TUnitRole> : IHasValidator<TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
    Task Insert(TUnitRole unitRole, CancellationToken cancellationToken = default);
    Task Update(TUnitRole unitRole, CancellationToken cancellationToken = default);
}

public interface IUnitRoleService<TUnitRole> : IUnitRoleService<Guid, TUnitRole>
    where TUnitRole : UnitRole
{
}