namespace uBeac.Identity;

public class Unit<TUnitKey> : IEntity<TUnitKey> where TUnitKey : IEquatable<TUnitKey>
{
    public virtual TUnitKey Id { get; set; }
    public virtual string Name { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string Code { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}

public class Unit : Unit<Guid>, IEntity
{
}