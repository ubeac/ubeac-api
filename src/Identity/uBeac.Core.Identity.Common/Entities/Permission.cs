namespace uBeac.Identity;

public class Permission<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string AreaName { get; set; }
    public virtual string ControllerName { get; set; }
    public virtual string ActionName { get; set; }
    public virtual string Role { get; set; }
    public virtual string UnitType { get; set; }
}

public class Permission : Permission<Guid>, IEntity
{
}