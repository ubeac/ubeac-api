namespace uBeac.Identity;

public static class UnitIdByIdentifiersResultExtensions
{
    public static TKey GetId<TKey, TUnit>(this IEnumerable<UnitIdByIdentifiersResult<TKey>> ids, TUnit unit)
        where TKey : IEquatable<TKey>
        where TUnit : Unit<TKey>
        => ids.First(result => result.Identifiers.Equals<TKey, TUnit>(unit)).Id;
}