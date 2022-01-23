namespace uBeac.Identity;

public class UnitRole<TUnitRoleKey> : IEntity<TUnitRoleKey> where TUnitRoleKey : IEquatable<TUnitRoleKey>
{
    public virtual TUnitRoleKey Id { get; set; }
    public virtual string UserName { get; set; }
    public virtual string UnitCode { get; set; }
    public virtual string UnitType { get; set; }
    public virtual string Role { get; set; }
}

public class UnitRole : UnitRole<Guid>, IEntity
{
}