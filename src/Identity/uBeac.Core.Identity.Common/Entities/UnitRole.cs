namespace uBeac.Identity;

public class UnitRole<TUnitRoleKey> : IEntity<TUnitRoleKey> where TUnitRoleKey : IEquatable<TUnitRoleKey>
{
    public UnitRole(TUnitRoleKey id, string unitCode, string type, string role)
    {
        Id = id;
        UnitCode = unitCode;
        Type = type;
        Role = role;
    }

    public virtual TUnitRoleKey Id { get; set; }
    [Identifier] public virtual string UnitCode { get; set; }
    [Identifier] public virtual string Type { get; set; }
    [Identifier] public virtual string Role { get; set; }
}

public class UnitRole : UnitRole<Guid>, IEntity
{
    public UnitRole(string unitCode, string type, string role) : base(Guid.NewGuid(), unitCode, type, role)
    {
    }
}