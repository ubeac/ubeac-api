using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;

namespace uBeac.Identity.EntityFramework;

public class EFUnitRepository<TUnitKey, TUnit, TContext> : EFEntityRepository<TUnitKey, TUnit, TContext>, IUnitRepository<TUnitKey, TUnit>
    where TUnitKey : IEquatable<TUnitKey>
    where TUnit : Unit<TUnitKey>
    where TContext : DbContext
{
    public EFUnitRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnitKey, TUnit> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }

    public async Task<IEnumerable<TUnit>> GetByParentId(TUnitKey parentUnitId, CancellationToken cancellationToken = default)
        => await Task.Run(() => AsQueryable().Where(unit => unit.ParentUnitId.Equals(parentUnitId)), cancellationToken);
}

public class EFUnitRepository<TUnit, TContext> : EFUnitRepository<Guid, TUnit, TContext>, IUnitRepository<TUnit>
    where TUnit : Unit
    where TContext : DbContext
{
    public EFUnitRepository(TContext dbContext, IApplicationContext applicationContext, IEntityEventManager<TUnit> eventManager) : base(dbContext, applicationContext, eventManager)
    {
    }
}
