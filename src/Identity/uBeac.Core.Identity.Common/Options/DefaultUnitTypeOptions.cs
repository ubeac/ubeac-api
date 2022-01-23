namespace uBeac.Identity;

public class DefaultUnitTypeOptions<TKey, TUnitType> : DefaultOptions<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
}

public class DefaultUnitTypeOptions<TUnitType> : DefaultUnitTypeOptions<Guid, TUnitType>
    where TUnitType : UnitType
{
}