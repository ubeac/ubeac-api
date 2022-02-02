namespace uBeac.Identity;

public class ReplaceUnit<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}

public class ReplaceUnit : ReplaceUnit<Guid>
{
}