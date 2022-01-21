namespace uBeac.Identity;

public static class UnitTypeExtensions
{
    public static UnitTypeIdentifiers GetIdentifiers<TUnitTypeKey, TUnitType>(this TUnitType unitType)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        return new UnitTypeIdentifiers(unitType.Code);
    }

    public static UnitTypeIdentifiers GetIdentifiers<TUnitType>(this TUnitType unitType)
        where TUnitType : UnitType
    {
        return GetIdentifiers<Guid, TUnitType>(unitType);
    }

    public static IEnumerable<UnitTypeIdentifiers> GetIdentifiers<TUnitTypeKey, TUnitType>(this IEnumerable<TUnitType> unitTypes)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        return unitTypes.Distinct().Select(unitType => new UnitTypeIdentifiers(unitType.Code));
    }

    public static IEnumerable<UnitTypeIdentifiers> GetIdentifiers<TUnitType>(this IEnumerable<TUnitType> unitTypes)
        where TUnitType : UnitType
    {
        return GetIdentifiers<Guid, TUnitType>(unitTypes);
    }

    public static UnitTypeIdByIdentifiersResult<TUnitTypeKey> GetIdResult<TUnitTypeKey, TUnitType>(this TUnitType unitType)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {

        return new UnitTypeIdByIdentifiersResult<TUnitTypeKey>(unitType.Id, GetIdentifiers<TUnitTypeKey, TUnitType>(unitType));
    }

    public static UnitTypeIdByIdentifiersResult GetIdResult<TUnitType>(this TUnitType unitType)
        where TUnitType : UnitType
    {

        return new UnitTypeIdByIdentifiersResult(unitType.Id, GetIdentifiers(unitType));
    }
}