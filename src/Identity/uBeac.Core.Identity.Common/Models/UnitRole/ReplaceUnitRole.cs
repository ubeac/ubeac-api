namespace uBeac.Identity;

public class ReplaceUnitRole<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string UserName { get; set; }
    public virtual string UnitCode { get; set; }
    public virtual string UnitType { get; set; }
    public virtual string Role { get; set; }
}

public class ReplaceUnitRole : ReplaceUnitRole<Guid>
{
}