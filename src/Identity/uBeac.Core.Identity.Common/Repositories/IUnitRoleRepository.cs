using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRoleRepository<TKey, TUnitRole> : IEntityRepository<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
}

public interface IUnitRoleRepository<TUnitRole> : IUnitRoleRepository<Guid, TUnitRole>, IEntityRepository<TUnitRole>
    where TUnitRole : UnitRole
{
}