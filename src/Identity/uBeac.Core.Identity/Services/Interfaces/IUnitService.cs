using uBeac.Services;

namespace uBeac.Identity;

public interface IUnitService<TKey, TUnit> : IEntityService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task<bool> Exists(string code, string type, CancellationToken cancellationToken = default);
}

public interface IUnitService<TUnit> : IUnitService<Guid, TUnit>
    where TUnit : Unit
{
}