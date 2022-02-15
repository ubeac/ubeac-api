namespace uBeac.Identity;

public class UnitOptions<TKey, TUnit> : IOptions<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    public IEnumerable<TUnit> DefaultValues { get; set; }
}

public class UnitOptions<TUnit> : UnitOptions<Guid, TUnit>, IOptions<TUnit>
    where TUnit : Unit
{
}