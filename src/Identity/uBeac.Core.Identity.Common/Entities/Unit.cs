namespace uBeac.Identity;

public class Unit<TUnitKey> : IEntity<TUnitKey> where TUnitKey : IEquatable<TUnitKey>
{
    public virtual TUnitKey Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string Type { get; set; }
    public virtual string? Description { get; set; }
    public virtual string? ParentUnitId { get; set; }
}

public class Unit : Unit<Guid>, IEntity
{
}