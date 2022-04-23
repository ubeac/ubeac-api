using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity;

public class Role<TRoleKey> : IdentityRole<TRoleKey>, IAuditEntity<TRoleKey> where TRoleKey : IEquatable<TRoleKey>
{
    public virtual string Description { get; set; }
    public virtual List<IdentityRoleClaim<TRoleKey>> Claims { get; set; } = new();

    public virtual string CreatedBy { get; set; }
    public virtual string CreatedByIp { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual string LastUpdatedBy { get; set; }
    public virtual string LastUpdatedByIp { get; set; }
    public virtual DateTime? LastUpdatedAt { get; set; }
    public virtual IApplicationContext Context { get; set; }

    public Role()
    {
    }

    public Role(string name) : base(name)
    {
    }

    public override string ToString()
    {
        return Name;
    }
}

public class Role : Role<Guid>, IAuditEntity
{
    public Role()
    {
    }

    public Role(string name) : base(name)
    {
    }
}