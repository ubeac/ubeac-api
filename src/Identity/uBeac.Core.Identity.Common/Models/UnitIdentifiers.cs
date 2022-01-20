namespace uBeac.Identity;

public class UnitIdentifiers
{
    public UnitIdentifiers(string code, string type)
    {
        Code = code;
        Type = type;
    }

    public string Code { get; set; }
    public string Type { get; set; }

    public bool Equals<TKey, TUnit>(TUnit unit)
        where TKey : IEquatable<TKey>
        where TUnit : Unit<TKey>
    {
        if (unit.Code != Code) return false;
        if (unit.Type != Type) return false;
        return true;
    }
}