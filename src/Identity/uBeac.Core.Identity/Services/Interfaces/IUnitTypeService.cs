namespace uBeac.Identity;

public interface IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
}

public interface IUnitTypeService<TUnitType> : IUnitTypeService<Guid, TUnitType>
    where TUnitType : UnitType
{
}