namespace uBeac.Identity;

public class DefaultUnitOptions<TKey, TUnit> : DefaultOptions<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
}

public class DefaultUnitOptions<TUnit> : DefaultUnitOptions<Guid, TUnit>
    where TUnit : Unit
{
}