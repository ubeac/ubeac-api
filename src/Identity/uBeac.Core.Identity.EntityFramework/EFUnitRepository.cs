using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace uBeac.Identity.EntityFramework;

public class EFUnitRepository<TUnitKey, TUnit, TContext> : EFEntityRepository<TUnitKey, TUnit, TContext>, IUnitRepository<TUnitKey, TUnit>
    where TUnitKey : IEquatable<TUnitKey>
    where TUnit : Unit<TUnitKey>
    where TContext : DbContext
{
    public EFUnitRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }

    public async Task<IEnumerable<TUnit>> GetByParentId(TUnitKey parentUnitId, CancellationToken cancellationToken = default)
        => await Task.Run(() => AsQueryable().Where(unit => unit.ParentUnitId.Equals(parentUnitId)), cancellationToken);
}

public class EFUnitRepository<TUnit, TContext> : EFUnitRepository<Guid, TUnit, TContext>, IUnitRepository<TUnit>
    where TUnit : Unit
    where TContext : DbContext
{
    public EFUnitRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
    {
    }
}
