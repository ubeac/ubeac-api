using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFUnitTypeRepository<TUnitTypeKey, TUnitType, TContext> : EFEntityRepository<TUnitTypeKey, TUnitType, TContext>, IUnitTypeRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey: IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
    where TContext : DbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnitTypeKey, TUnitType> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}

public class EFUnitTypeRepository<TUnitType, TContext> : EFUnitTypeRepository<Guid, TUnitType, TContext>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
    where TContext : DbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnitType> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}