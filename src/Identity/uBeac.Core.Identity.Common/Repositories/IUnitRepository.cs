using uBeac.Repositories;

namespace uBeac.Identity;

public interface IUnitRepository<TKey, TUnit> : IEntityRepository<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task<bool> Any(string code, string type);
}

public interface IUnitRepository<TUnit> : IUnitRepository<Guid, TUnit>, IEntityRepository<TUnit>
    where TUnit : Unit
{
}