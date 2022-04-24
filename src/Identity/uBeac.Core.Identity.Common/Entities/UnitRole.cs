namespace uBeac.Identity;

public class UnitRole<TUnitRoleKey> : Entity<TUnitRoleKey> where TUnitRoleKey : IEquatable<TUnitRoleKey>
{
    public virtual string? UserName { get; set; }
    public virtual string? UnitCode { get; set; }
    public virtual string? UnitType { get; set; }
    public virtual string? Role { get; set; }
}

public class UnitRole : UnitRole<Guid>, IEntity
{
}