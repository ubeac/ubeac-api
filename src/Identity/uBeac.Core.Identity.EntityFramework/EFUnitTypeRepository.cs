using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUnitTypeRepository<TUnitTypeKey, TUnitType, TContext> : EFEntityRepository<TUnitTypeKey, TUnitType, TContext>, IUnitTypeRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey: IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
    where TContext : DbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}

public class EFUnitTypeRepository<TUnitType, TContext> : EFUnitTypeRepository<Guid, TUnitType, TContext>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
    where TContext : DbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}