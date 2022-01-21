namespace uBeac.Identity;

public static class UnitExtensions
{
    public static UnitIdentifiers GetIdentifiers<TUnitKey, TUnit>(this TUnit unit)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        return new UnitIdentifiers(unit.Code, unit.Type);
    }

    public static UnitIdentifiers GetIdentifiers<TUnit>(this TUnit unit)
        where TUnit : Unit
    {
        return GetIdentifiers<Guid, TUnit>(unit);
    }

    public static IEnumerable<UnitIdentifiers> GetIdentifiers<TUnitKey, TUnit>(this IEnumerable<TUnit> units)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        return units.Distinct().Select(unit => new UnitIdentifiers(unit.Code, unit.Type));
    }

    public static IEnumerable<UnitIdentifiers> GetIdentifiers<TUnit>(this IEnumerable<TUnit> units)
        where TUnit : Unit
    {
        return GetIdentifiers<Guid, TUnit>(units);
    }

    public static UnitIdByIdentifiersResult<TUnitKey> GetIdResult<TUnitKey, TUnit>(this TUnit unit)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {

        return new UnitIdByIdentifiersResult<TUnitKey>(unit.Id, GetIdentifiers<TUnitKey, TUnit>(unit));
    }

    public static UnitIdByIdentifiersResult GetIdResult<TUnit>(this TUnit unit)
        where TUnit : Unit
    {

        return new UnitIdByIdentifiersResult(unit.Id, GetIdentifiers(unit));
    }
}