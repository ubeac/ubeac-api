﻿using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUnitTypeRepository<TUnitTypeKey, TUnitType, TContext> : EFEntityRepository<TUnitTypeKey, TUnitType, TContext>, IUnitTypeRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey: IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
    where TContext : EFDbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}

public class EFUnitTypeRepository<TUnitType, TContext> : EFUnitTypeRepository<Guid, TUnitType, TContext>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
    where TContext : EFDbContext
{
    public EFUnitTypeRepository(TContext dbContext, IApplicationContext applicationContext, HistoryFactory historyFactory) : base(dbContext, applicationContext, historyFactory)
    {
    }
}