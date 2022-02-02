namespace uBeac.Identity;

public class ReplaceUnitType<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string Code { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
}

public class ReplaceUnitType : ReplaceUnitType<Guid>
{
}