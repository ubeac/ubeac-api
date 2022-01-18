using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : HasValidator<TUnit>, IUnitService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitRepository<TKey, TUnit> UnitRepository;

    public UnitService(IUnitRepository<TKey, TUnit> unitRepository, IEnumerable<IValidator<TUnit>> validators) : base(validators.ToList())
    {
        UnitRepository = unitRepository;
    }

    public async Task Insert(TUnit unit, CancellationToken cancellationToken = default)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unit);
        await ThrowIfInvalid(unit);

        await UnitRepository.Insert(unit, cancellationToken);
    }

    private void ThrowIfNull(TUnit unit)
    {
        if (unit == null) throw new ArgumentNullException(nameof(unit));
    }

    private void ThrowIfCancelled(CancellationToken cancellationToken)
        => cancellationToken.ThrowIfCancellationRequested();

    private async Task ThrowIfInvalid(TUnit unit)
        => (await Validate(unit)).ThrowIfInvalid();
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> unitRepository, IEnumerable<IValidator<TUnit>> validators) : base(unitRepository, validators.ToList())
    {
    }
}