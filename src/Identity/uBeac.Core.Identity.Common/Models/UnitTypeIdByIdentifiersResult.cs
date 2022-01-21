namespace uBeac.Identity;

public class UnitTypeIdByIdentifiersResult<TKey> where TKey : IEquatable<TKey>
{
    public UnitTypeIdByIdentifiersResult(TKey id, UnitTypeIdentifiers identifiers)
    {
        Id = id;
        Identifiers = identifiers;
    }

    public TKey Id { get; set; }
    public UnitTypeIdentifiers Identifiers { get; set; }
}

public class UnitTypeIdByIdentifiersResult : UnitTypeIdByIdentifiersResult<Guid>
{
    public UnitTypeIdByIdentifiersResult(Guid id, UnitTypeIdentifiers identifiers) : base(id, identifiers)
    {
    }
}