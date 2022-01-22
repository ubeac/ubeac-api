using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRoleRepository<TUnitRoleKey, TUnitRole> : IEntityRepository<TUnitRoleKey, TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
}

public interface IUnitRoleRepository<TUnitRole> : IUnitRoleRepository<Guid, TUnitRole>, IEntityRepository<TUnitRole>
    where TUnitRole : UnitRole
{
}