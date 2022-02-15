using uBeac.Services;

namespace uBeac.Identity;

public class UnitRoleService<TKey, TUnitRole> : EntityService<TKey, TUnitRole>, IUnitRoleService<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
    public UnitRoleService(IUnitRoleRepository<TKey, TUnitRole> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}

public class UnitRoleService<TUnitRole> : UnitRoleService<Guid, TUnitRole>, IUnitRoleService<TUnitRole>
    where TUnitRole : UnitRole
{
    public UnitRoleService(IUnitRoleRepository<TUnitRole> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}