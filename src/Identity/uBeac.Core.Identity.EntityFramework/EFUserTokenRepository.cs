using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUserTokenRepository<TUserKey, TContext> : EFEntityRepository<TUserKey, UserToken<TUserKey>, TContext>, IUserTokenRepository<TUserKey>
    where TUserKey : IEquatable<TUserKey>
    where TContext : EFDbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}

public class EFUserTokenRepository<TContext> : EFUserTokenRepository<Guid, TContext>, IUserTokenRepository
    where TContext : EFDbContext
{
    public EFUserTokenRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}