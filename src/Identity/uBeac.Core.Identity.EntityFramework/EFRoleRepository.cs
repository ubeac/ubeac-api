using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFRoleRepository<TRoleKey, TRole, TContext> : EFEntityRepository<TRoleKey, TRole, TContext>, IRoleRepository<TRoleKey, TRole>
    where TRoleKey : IEquatable<TRoleKey>
    where TRole : Role<TRoleKey>
    where TContext : DbContext
{
    public EFRoleRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TRoleKey, TRole> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}

public class EFRoleRepository<TRole, TContext> : EFRoleRepository<Guid, TRole, TContext>, IRoleRepository<TRole>
    where TRole : Role
    where TContext : DbContext
{
    public EFRoleRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TRole> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}