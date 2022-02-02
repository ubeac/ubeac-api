using uBeac.Repositories;
using uBeac.Services;

namespace uBeac.Identity;

public class UnitRoleService<TKey, TUnitRole> : EntityService<TKey, TUnitRole>, IUnitRoleService<TKey, TUnitRole>
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
    public UnitRoleService(IUnitRoleRepository<TKey, TUnitRole> repository) : base(repository)
    {
    }

    public async Task Insert(InsertUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnitRole>();
        entity.UserName = unitRole.UserName;
        entity.UnitCode = unitRole.UnitCode;
        entity.UnitType = unitRole.UnitType;
        entity.Role = unitRole.Role;
        await Insert(entity, cancellationToken);
    }

    public async Task Replace(ReplaceUnitRole<TKey> unitRole, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnitRole>();
        entity.Id = unitRole.Id;
        entity.UserName = unitRole.UserName;
        entity.UnitCode = unitRole.UnitCode;
        entity.UnitType = unitRole.UnitType;
        entity.Role = unitRole.Role;
        await Replace(entity, cancellationToken);
    }
}

public class UnitRoleService<TUnitRole> : UnitRoleService<Guid, TUnitRole>, IUnitRoleService<TUnitRole>
    where TUnitRole : UnitRole
{
    public UnitRoleService(IUnitRoleRepository<TUnitRole> repository) : base(repository)
    {
    }
}