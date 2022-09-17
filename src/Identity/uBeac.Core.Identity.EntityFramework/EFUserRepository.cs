using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFUserRepository<TUserKey, TUser, TContext> : EFEntityRepository<TUserKey, TUser, TContext>, IUserRepository<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
    where TContext : DbContext
{
    public EFUserRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUserKey, TUser> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}

public class EFUserRepository<TUser, TContext> : EFUserRepository<Guid, TUser, TContext>, IUserRepository<TUser>
    where TUser : User
    where TContext : DbContext
{
    public EFUserRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUser> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}