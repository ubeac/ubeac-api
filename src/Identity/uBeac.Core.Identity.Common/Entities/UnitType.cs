namespace uBeac.Identity;

public class UnitType<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
{
    public virtual string? Code { get; set; }
    public virtual string? Name { get; set; }
    public virtual string? Description { get; set; }
}

public class UnitType : UnitType<Guid>, IEntity
{
}