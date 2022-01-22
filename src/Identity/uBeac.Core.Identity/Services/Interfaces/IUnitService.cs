namespace uBeac.Identity;

public interface IUnitService<TKey, TUnit> : IHasValidator<TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task Insert(TUnit unit, CancellationToken cancellationToken = default);
    Task InsertMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default);
    Task Update(TUnit unit, CancellationToken cancellationToken = default);
    Task UpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default);
    Task InsertOrUpdateMany(IEnumerable<TUnit> units, CancellationToken cancellationToken = default);
    Task Remove(TKey unitId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUnit>> GetAll(CancellationToken cancellationToken = default);
}

public interface IUnitService<TUnit> : IUnitService<Guid, TUnit>
    where TUnit : Unit
{
}