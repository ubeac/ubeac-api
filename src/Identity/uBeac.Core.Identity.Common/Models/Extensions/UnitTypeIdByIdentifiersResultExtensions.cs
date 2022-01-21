namespace uBeac.Identity;

public static class UnitTypeIdByIdentifiersResultExtensions
{
    public static TKey GetId<TKey, TUnitType>(this IEnumerable<UnitTypeIdByIdentifiersResult<TKey>> ids, TUnitType unitType)
        where TKey : IEquatable<TKey>
        where TUnitType : UnitType<TKey>
        => ids.First(result => result.Identifiers.Equals<TKey, TUnitType>(unitType)).Id;
}