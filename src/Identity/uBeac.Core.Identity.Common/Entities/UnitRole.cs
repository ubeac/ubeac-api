namespace uBeac.Identity;

public class UnitRole<TUnitRoleKey> : IEntity<TUnitRoleKey> where TUnitRoleKey : IEquatable<TUnitRoleKey>
{
    public virtual TUnitRoleKey Id { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string UserName { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string UnitCode { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string UnitType { get; set; }
    // TODO: do we really need the Identifier attribute?
    [Identifier] public virtual string Role { get; set; }
}

public class UnitRole : UnitRole<Guid>, IEntity
{
}