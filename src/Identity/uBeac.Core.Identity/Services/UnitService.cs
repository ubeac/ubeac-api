namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : IUnitService<TKey, TUnit> 
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
}