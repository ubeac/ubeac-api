namespace uBeac.Identity;

public class Unit<TUnitKey> : IEntity<TUnitKey> where TUnitKey : IEquatable<TUnitKey>
{
    public Unit(TUnitKey id, string name, string code, string type, string parentUnitId = null)
    {
        Id = id;
        Name = name;
        Code = code;
        Type = type;
        ParentUnitId = parentUnitId;
    }

    public virtual TUnitKey Id { get; set; }
    public virtual string Name { get; set; }
    [Identifier] public virtual string Code { get; set; }
    [Identifier] public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}

public class Unit : Unit<Guid>, IEntity
{
    public Unit(string name, string code, string type, string parentUnitId = null) : base(Guid.NewGuid(), name, code, type, parentUnitId)
    {
    }
}