namespace uBeac.Identity;

public class UnitTypeOptions<TKey, TUnitType> : IOptions<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    public IEnumerable<TUnitType>? DefaultValues { get; set; }
}

public class UnitTypeOptions<TUnitType> : UnitTypeOptions<Guid, TUnitType>, IOptions<TUnitType>
    where TUnitType : UnitType
{
}