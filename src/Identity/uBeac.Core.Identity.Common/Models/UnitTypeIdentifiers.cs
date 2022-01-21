namespace uBeac.Identity;

public class UnitTypeIdentifiers
{
    public UnitTypeIdentifiers(string code)
    {
        Code = code;
    }

    public string Code { get; set; }

    public bool Equals<TKey, TUnitType>(TUnitType unitType)
        where TKey : IEquatable<TKey>
        where TUnitType : UnitType<TKey>
    {
        if (unitType.Code != Code) return false;
        return true;
    }
}