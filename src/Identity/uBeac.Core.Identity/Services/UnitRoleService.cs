using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitRoleService<TUnitRoleKey, TUnitRole> : HasValidator<TUnitRole>, IUnitRoleService<TUnitRoleKey, TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
    protected readonly IUnitRoleRepository<TUnitRoleKey, TUnitRole> UnitRoleRepository;

    public UnitRoleService(IUnitRoleRepository<TUnitRoleKey, TUnitRole> unitRoleRepository, IEnumerable<IValidator<TUnitRole>> validators) : base(validators.ToList())
    {
        UnitRoleRepository = unitRoleRepository;
    }

    public virtual async Task Insert(TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        await BeforeInsert(unitRole, cancellationToken);
        await UnitRoleRepository.Insert(unitRole, cancellationToken);
    }

    protected virtual async Task BeforeInsert(TUnitRole unitRole, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitRole);
        await ThrowIfInvalid(unitRole);
    }

    public virtual async Task Update(TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        await BeforeUpdate(unitRole, cancellationToken);
        unitRole = await UnitRoleRepository.CorrectId(unitRole, cancellationToken);
        await UnitRoleRepository.Replace(unitRole, cancellationToken);
    }

    protected virtual async Task BeforeUpdate(TUnitRole unitRole, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitRole);
        await ThrowIfInvalid(unitRole);
        await ThrowIfNotExists(unitRole, cancellationToken);
    }

    protected virtual void ThrowIfNull(TUnitRole unitRole)
    {
        if (unitRole == null) throw new ArgumentNullException(nameof(unitRole));
    }

    protected virtual async Task ThrowIfNotExists(TUnitRole unitRole, CancellationToken cancellationToken)
    {
        if (!await UnitRoleRepository.Any(unitRole, cancellationToken)) throw new ArgumentException($"{nameof(unitRole)} is not exists.");
    }

    protected virtual void ThrowIfCancelled(CancellationToken cancellationToken)
        => cancellationToken.ThrowIfCancellationRequested();

    protected virtual async Task ThrowIfInvalid(TUnitRole unitRole)
        => (await Validate(unitRole)).ThrowIfInvalid();
}

public class UnitRoleService<TUnitRole> : UnitRoleService<Guid, TUnitRole>, IUnitRoleService<TUnitRole>
    where TUnitRole : UnitRole
{
    public UnitRoleService(IUnitRoleRepository<TUnitRole> unitRoleRepository, IEnumerable<IValidator<TUnitRole>> validators) : base(unitRoleRepository, validators)
    {
    }
}