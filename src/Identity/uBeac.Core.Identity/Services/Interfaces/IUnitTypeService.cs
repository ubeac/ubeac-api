namespace uBeac.Identity;

public interface IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    Task Insert(TUnitType unitType, CancellationToken cancellationToken = default);
    Task InsertMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default);
    Task Update(TUnitType unitType, CancellationToken cancellationToken = default);
    Task UpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default);
    Task InsertOrUpdateMany(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default);
    Task Remove(TKey unitTypeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUnitType>> GetAll(CancellationToken cancellationToken = default);
}

public interface IUnitTypeService<TUnitType> : IUnitTypeService<Guid, TUnitType>
    where TUnitType : UnitType
{
}