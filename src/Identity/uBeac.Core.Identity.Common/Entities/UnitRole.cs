namespace uBeac.Identity;

public class UnitRole<TUnitRoleKey> : IEntity<TUnitRoleKey> where TUnitRoleKey : IEquatable<TUnitRoleKey>
{
    public UnitRole(TUnitRoleKey id, string userName, string unitCode, string unitType, string role)
    {
        Id = id;
        UserName = userName;
        UnitCode = unitCode;
        UnitType = unitType;
        Role = role;
    }

    public virtual TUnitRoleKey Id { get; set; }
    [Identifier] public virtual string UserName { get; set; }
    [Identifier] public virtual string UnitCode { get; set; }
    [Identifier] public virtual string UnitType { get; set; }
    [Identifier] public virtual string Role { get; set; }
}

public class UnitRole : UnitRole<Guid>, IEntity
{
    public UnitRole(string userName, string unitCode, string type, string role) : base(Guid.NewGuid(), userName, unitCode, type, role)
    {
    }
}