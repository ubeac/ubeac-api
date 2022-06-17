using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;

namespace API;

public class EFIdentityDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public EFIdentityDbContext(DbContextOptions options) : base(options)
    {
    }
    
    // public DbSet<Unit> Units { get; set; }
    // public DbSet<UnitType> UnitTypes { get; set; }
    // public DbSet<UnitRole> UnitRoles { get; set; }
    // public DbSet<User> Users { get; set; }
    // public DbSet<Role> Roles { get; set; }
}