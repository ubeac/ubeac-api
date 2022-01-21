using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitTypeRepository<TUnitTypeKey, TUnitType> : IEntityRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
{
    Task<UnitTypeIdByIdentifiersResult<TUnitTypeKey>> GetId(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken = default);
    Task<IEnumerable<UnitTypeIdByIdentifiersResult<TUnitTypeKey>>> GetIds(IEnumerable<UnitTypeIdentifiers> identifiers, CancellationToken cancellationToken = default);
    Task<bool> Any(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken = default);
}

public interface IUnitTypeRepository<TUnitType> : IUnitTypeRepository<Guid, TUnitType>, IEntityRepository<TUnitType>
    where TUnitType : UnitType
{
}