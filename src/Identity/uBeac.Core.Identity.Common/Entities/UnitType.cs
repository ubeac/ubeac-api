namespace uBeac.Identity;

public class UnitType<TUnitTypeKey> : IEntity<TUnitTypeKey> where TUnitTypeKey : IEquatable<TUnitTypeKey>
{
    public virtual TUnitTypeKey Id { get; set; }
    public virtual string Code { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
}

public class UnitType : UnitType<Guid>, IEntity
{
}