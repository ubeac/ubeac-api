using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitTypeService<TKey, TUnitType> : HasValidator<TUnitType>, IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeRepository<TKey, TUnitType> UnitTypeRepository;

    public UnitTypeService(IUnitTypeRepository<TKey, TUnitType> unitTypeRepository, IEnumerable<IValidator<TUnitType>> validators) : base(validators.ToList())
    {
        UnitTypeRepository = unitTypeRepository;
    }

    public virtual async Task Insert(TUnitType unitType, CancellationToken cancellationToken = default)
    {
        await BeforeInsert(unitType, cancellationToken);
        await UnitTypeRepository.Insert(unitType, cancellationToken);
    }

    protected virtual async Task BeforeInsert(TUnitType unitType, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitType);
        await ThrowIfInvalid(unitType);
    }

    public virtual async Task InsertMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default)
    {
        await BeforeInsertMany(unitTypes, cancellationToken);
        await UnitTypeRepository.InsertMany(unitTypes, cancellationToken);
    }

    protected virtual async Task BeforeInsertMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitTypes);

        var beforeInsertPerUnitType = unitTypes.Select(e => BeforeInsert(e, cancellationToken));
        await Task.WhenAll(beforeInsertPerUnitType);
    }

    public virtual async Task Update(TUnitType unitType, CancellationToken cancellationToken = default)
    {
        await BeforeUpdate(unitType, cancellationToken);
        unitType = await ReplaceIdByIdentifiers(unitType, cancellationToken);
        await UnitTypeRepository.Replace(unitType, cancellationToken);
    }

    protected virtual async Task BeforeUpdate(TUnitType unitType, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitType);
        await ThrowIfInvalid(unitType);
        await ThrowIfNotExists(unitType, cancellationToken);
    }

    public virtual async Task UpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default)
    {
        await BeforeUpdateMany(unitTypes, cancellationToken);
        unitTypes = await ReplaceIdsByIdentifiers(unitTypes, cancellationToken);
        await UnitTypeRepository.ReplaceMany(unitTypes, cancellationToken);
    }

    protected virtual async Task BeforeUpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitTypes);

        var beforeUpdatePerUnit = unitTypes.Select(e => BeforeUpdate(e, cancellationToken));
        await Task.WhenAll(beforeUpdatePerUnit);
    }

    public virtual async Task InsertOrUpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default)
    {
        await BeforeInsertOrUpdateMany(unitTypes, cancellationToken);

        unitTypes = await ReplaceIdsByIdentifiers(unitTypes, cancellationToken);
        var insertUnitTypes = unitTypes.Where(unitType => unitType.Id == null);
        var updateUnitTypes = unitTypes.Where(unitType => unitType.Id != null);
        await InsertMany(insertUnitTypes, cancellationToken);
        await UpdateMany(updateUnitTypes, cancellationToken);
    }

    protected virtual async Task BeforeInsertOrUpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        ThrowIfNull(unitTypes);
        await Task.CompletedTask;
    }

    protected virtual void ThrowIfNull(TUnitType unitType)
    {
        if (unitType == null) throw new ArgumentNullException(nameof(unitType));
    }

    protected virtual void ThrowIfNull(IEnumerable<TUnitType> unitTypes)
    {
        if (unitTypes == null) throw new ArgumentNullException(nameof(unitTypes));
    }

    protected virtual async Task ThrowIfNotExists(TUnitType unitType, CancellationToken cancellationToken)
    {
        var identifiers = GetIdentifiers(unitType);
        if (!await UnitTypeRepository.Any(identifiers, cancellationToken)) throw new ArgumentException($"{nameof(unitType)} is not exists.");
    }

    protected virtual void ThrowIfCancelled(CancellationToken cancellationToken)
        => cancellationToken.ThrowIfCancellationRequested();

    protected virtual async Task ThrowIfInvalid(TUnitType unitType)
        => (await Validate(unitType)).ThrowIfInvalid();

    protected virtual UnitTypeIdentifiers GetIdentifiers(TUnitType unitType) => unitType.GetIdentifiers<TKey, TUnitType>();
    protected virtual IEnumerable<UnitTypeIdentifiers> GetIdentifiers(IEnumerable<TUnitType> unitTypes) => unitTypes.GetIdentifiers<TKey, TUnitType>();

    protected virtual async Task<TUnitType> ReplaceIdByIdentifiers(TUnitType unitType, CancellationToken cancellationToken)
    {
        var identifiers = GetIdentifiers(unitType);
        unitType.Id = (await UnitTypeRepository.GetId(identifiers, cancellationToken)).Id;
        return unitType;
    }

    protected virtual async Task<IEnumerable<TUnitType>> ReplaceIdsByIdentifiers(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken)
    {
        var identifiers = GetIdentifiers(unitTypes);
        var ids = (await UnitTypeRepository.GetIds(identifiers, cancellationToken)).ToList();

        var updatedUnitTypes = unitTypes.ToList();
        for (int i = 0; i < updatedUnitTypes.Count; i++)
        {
            updatedUnitTypes[i].Id = ids.GetId(updatedUnitTypes[i]);
        }

        return updatedUnitTypes;
    }
}

public class UnitTypeService<TUnitType> : UnitTypeService<Guid, TUnitType>, IUnitTypeService<TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeService(IUnitTypeRepository<Guid, TUnitType> unitTypeRepository, IEnumerable<IValidator<TUnitType>> validators) : base(unitTypeRepository, validators.ToList())
    {
    }
}