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

    public virtual async Task Insert(TUnit unit, CancellationToken cancellationToken = default)
    {
        await BeforeInsert(unit, cancellationToken);
        await UnitRepository.Insert(unit, cancellationToken);
    }

    protected virtual async Task BeforeInsert(TUnit unit, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unit);
        await ThrowIfInvalid(unit);
    }

    public virtual async Task InsertMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default)
    {
        await BeforeInsertMany(units, cancellationToken);
        await UnitRepository.InsertMany(units, cancellationToken);
    }

    protected virtual async Task BeforeInsertMany(IEnumerable<TUnit> units, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(units);

        var beforeInsertPerUnit = units.Select(e => BeforeInsert(e, cancellationToken));
        await Task.WhenAll(beforeInsertPerUnit);
    }

    public virtual async Task Update(TUnit unit, CancellationToken cancellationToken = default)
    {
        await BeforeUpdate(unit, cancellationToken);
        unit = await UnitRepository.CorrectId(unit, cancellationToken);
        await UnitRepository.Replace(unit, cancellationToken);
    }

    protected virtual async Task BeforeUpdate(TUnit unit, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unit);
        await ThrowIfInvalid(unit);
        await ThrowIfNotExists(unit, cancellationToken);
    }

    public virtual async Task UpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default)
    {
        await BeforeUpdateMany(units, cancellationToken);
        units = await UnitRepository.CorrectId(units, cancellationToken);
        await UnitRepository.ReplaceMany(units, cancellationToken);
    }

    protected virtual async Task BeforeUpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(units);

        var beforeUpdatePerUnit = units.Select(e => BeforeUpdate(e, cancellationToken));
        await Task.WhenAll(beforeUpdatePerUnit);
    }

    public virtual async Task InsertOrUpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default)
    {
        await BeforeInsertOrUpdateMany(units, cancellationToken);

        units = await UnitRepository.CorrectId(units, cancellationToken);
        var insertUnits = units.Where(unit => unit.Id == null);
        var updateUnits = units.Where(unit => unit.Id != null);
        await InsertMany(insertUnits, cancellationToken);
        await UpdateMany(updateUnits, cancellationToken);
    }

    protected virtual async Task BeforeInsertOrUpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(units);
        await Task.CompletedTask;
    }

    protected virtual void ThrowIfNull(TUnit unit)
    {
        if (unit == null) throw new ArgumentNullException(nameof(unit));
    }

    protected virtual void ThrowIfNull(IEnumerable<TUnit> units)
    {
        if (units == null) throw new ArgumentNullException(nameof(units));
    }

    protected virtual async Task ThrowIfNotExists(TUnit unit, CancellationToken cancellationToken)
    {
        if (!await UnitRepository.Any(unit, cancellationToken)) throw new ArgumentException($"{nameof(unit)} is not exists.");
    }

    protected virtual void ThrowIfCancelled(CancellationToken cancellationToken)
        => cancellationToken.ThrowIfCancellationRequested();

    protected virtual async Task ThrowIfInvalid(TUnit unit)
        => (await Validate(unit)).ThrowIfInvalid();
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> unitRepository, IEnumerable<IValidator<TUnit>> validators) : base(unitRepository, validators.ToList())
    {
    }
}