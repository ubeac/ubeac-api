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

    public virtual async Task Remove(TUnitRoleKey unitRoleId, CancellationToken cancellationToken = default)
    {
        await BeforeRemove(unitRoleId, cancellationToken);
        await UnitRoleRepository.Delete(unitRoleId, cancellationToken);
    }

    public virtual async Task BeforeRemove(TUnitRoleKey unitRoleId, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitRoleId);
        await Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<TUnitRole>> GetAll(CancellationToken cancellationToken = default)
    {
        await BeforeGetAll(cancellationToken);
        return await UnitRoleRepository.GetAll(cancellationToken);
    }

    public virtual async Task BeforeGetAll(CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    protected virtual void ThrowIfNull(TUnitRole unitRole)
    {
        if (unitRole == null) throw new ArgumentNullException(nameof(unitRole));
    }

    protected virtual void ThrowIfNull(TUnitRoleKey unitRoleId)
    {
        if (unitRoleId == null) throw new ArgumentNullException(nameof(unitRoleId));
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