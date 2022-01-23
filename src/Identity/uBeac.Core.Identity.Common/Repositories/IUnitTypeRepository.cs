using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitTypeRepository<TUnitTypeKey, TUnitType> : IEntityRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
{
}

public interface IUnitTypeRepository<TUnitType> : IUnitTypeRepository<Guid, TUnitType>, IEntityRepository<TUnitType>
    where TUnitType : UnitType
{
}