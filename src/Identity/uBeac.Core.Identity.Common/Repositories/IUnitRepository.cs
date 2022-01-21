using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRepository<TKey, TUnit> : IEntityRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task<TUnit> CorrectId(TUnit unit, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUnit>> CorrectId(IEnumerable<TUnit> units, CancellationToken cancellationToken = default);
    Task<bool> Any(TUnit unit, CancellationToken cancellationToken = default);
}

public interface IUnitRepository<TUnit> : IUnitRepository<Guid, TUnit>, IEntityRepository<TUnit>
    where TUnit : Unit
{
}