namespace uBeac.Identity;

public class UnitType<TUnitTypeKey> : IEntity<TUnitTypeKey> where TUnitTypeKey : IEquatable<TUnitTypeKey>
{
    public virtual TUnitTypeKey Id { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string Code { get; set; }
    public virtual string Name { get; set; }
}

public class UnitType : UnitType<Guid>, IEntity
{
}