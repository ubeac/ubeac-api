using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitTypeRepository<TUnitTypeKey, TUnitType> : IEntityRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
{
    Task<TUnitType> CorrectId(TUnitType unitType, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUnitType>> CorrectId(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default);
    Task<bool> Any(TUnitType unitType, CancellationToken cancellationToken = default);
}

public interface IUnitTypeRepository<TUnitType> : IUnitTypeRepository<Guid, TUnitType>, IEntityRepository<TUnitType>
    where TUnitType : UnitType
{
}