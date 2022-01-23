using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitTypeRepository<TKey, TUnitType> : IEntityRepository<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
}

public interface IUnitTypeRepository<TUnitType> : IUnitTypeRepository<Guid, TUnitType>, IEntityRepository<TUnitType>
    where TUnitType : UnitType
{
}