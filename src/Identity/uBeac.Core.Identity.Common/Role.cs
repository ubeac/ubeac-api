using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public class Role<TRoleKey> : IdentityRole<TRoleKey>, IEntity<TRoleKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        public virtual List<IdentityRoleClaim<TRoleKey>> Claims { get; set; } = new();

        public Role()
        {
        }

        public Role(string name) : this()
        {
            Name = name;
            NormalizedName = name.ToUpperInvariant();
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class Role : Role<Guid>, IEntity
    {
    }
}
