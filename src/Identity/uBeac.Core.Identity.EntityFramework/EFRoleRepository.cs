using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFRoleRepository<TRoleKey, TRole, TContext> : EFEntityRepository<TRoleKey, TRole, TContext>, IRoleRepository<TRoleKey, TRole>
    where TRoleKey : IEquatable<TRoleKey>
    where TRole : Role<TRoleKey>
    where TContext : DbContext
{
    public EFRoleRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}

public class EFRoleRepository<TRole, TContext> : EFRoleRepository<Guid, TRole, TContext>, IRoleRepository<TRole>
    where TRole : Role
    where TContext : DbContext
{
    public EFRoleRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}