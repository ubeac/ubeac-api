using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRepository<TKey, TUnit> : IEntityRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task<UnitIdByIdentifiersResult<TKey>> GetId(UnitIdentifiers identifiers, CancellationToken cancellationToken = default);
    Task<IEnumerable<UnitIdByIdentifiersResult<TKey>>> GetIds(IEnumerable<UnitIdentifiers> identifiers, CancellationToken cancellationToken = default);
    Task<bool> Any(UnitIdentifiers identifiers, CancellationToken cancellationToken = default);
}

public interface IUnitRepository<TUnit> : IUnitRepository<Guid, TUnit>, IEntityRepository<TUnit>
    where TUnit : Unit
{
}