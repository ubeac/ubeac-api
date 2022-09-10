using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUserTokenRepository<TUserKey, TContext> : EFEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : DbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}

public class EFUserTokenRepository<TContext> : EFUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : DbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}