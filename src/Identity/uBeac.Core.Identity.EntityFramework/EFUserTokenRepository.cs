using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFUserTokenRepository<TUserKey, TContext> : EFEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : DbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUserKey, UserToken<TUserKey>> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}

public class EFUserTokenRepository<TContext> : EFUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : DbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<Guid, UserToken<Guid>> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}