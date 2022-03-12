using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRepository<TKey, TUnit> : IEntityRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task<IEnumerable<TUnit>> GetByParentId(TKey parentUnitId, CancellationToken cancellationToken = default);
}

public interface IUnitRepository<TUnit> : IUnitRepository<Guid, TUnit>, IEntityRepository<TUnit>
    where TUnit : Unit
{
}