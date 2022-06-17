using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUserRepository<TUserKey, TUser, TContext> : EFEntityRepository<TUserKey, TUser, TContext>, IUserRepository<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
    where TContext : DbContext
{
    public EFUserRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}

public class EFUserRepository<TUser, TContext> : EFUserRepository<Guid, TUser, TContext>, IUserRepository<TUser>
    where TUser : User
    where TContext : DbContext
{
    public EFUserRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}