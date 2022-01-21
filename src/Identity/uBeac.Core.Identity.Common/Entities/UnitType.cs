namespace uBeac.Identity;

public class UnitType<TUnitTypeKey> : IEntity<TUnitTypeKey> where TUnitTypeKey : IEquatable<TUnitTypeKey>
{
    public UnitType(TUnitTypeKey id, string code, string name)
    {
        Id = id;
        Code = code;
        Name = name;
    }

    public virtual TUnitTypeKey Id { get; set; }
    [Identifier] public virtual string Code { get; set; }
    public virtual string Name { get; set; }
}

public class UnitType : UnitType<Guid>, IEntity
{
    public UnitType(string code, string name) : base(Guid.NewGuid(), code, name)
    {
    }
}