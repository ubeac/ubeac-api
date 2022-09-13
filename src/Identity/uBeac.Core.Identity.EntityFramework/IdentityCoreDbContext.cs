using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace uBeac.Identity.EntityFramework;

public class IdentityCoreDbContext<TUser, TRole, TKey>: IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
    where TUser : User<TKey>
    where TRole : Role<TKey>
    where TKey : IEquatable<TKey>
{
    public IdentityCoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public IdentityCoreDbContext()
    {
    }

    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<UnitType> UnitTypes { get; set; }
    public virtual DbSet<UnitRole> UnitRoles { get; set; }
}

public class IdentityCoreDbContext : IdentityCoreDbContext<User, Role, Guid>
{
    public IdentityCoreDbContext(DbContextOptions<IdentityCoreDbContext> options) : base(options)
    {
    }

    public IdentityCoreDbContext()
    {
    }
}