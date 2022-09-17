using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFUnitRoleRepository<TUnitRoleKey, TUnitRole, TContext> : EFEntityRepository<TUnitRoleKey, TUnitRole, TContext>, IUnitRoleRepository<TUnitRoleKey, TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
    where TContext : DbContext
{
    public EFUnitRoleRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnitRoleKey, TUnitRole> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}

public class EFUnitRoleRepository<TUnitRole, TContext> : EFUnitRoleRepository<Guid, TUnitRole, TContext>, IUnitRoleRepository<TUnitRole>
    where TUnitRole : UnitRole
    where TContext : DbContext
{
    public EFUnitRoleRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnitRole> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}