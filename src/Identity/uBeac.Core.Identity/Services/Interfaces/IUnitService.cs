namespace uBeac.Identity;

public interface IUnitService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task Insert(TUnit unit, CancellationToken cancellationToken = default);
}

public interface IUnitService<TUnit> : IUnitService<Guid, TUnit>
    where TUnit : Unit
{
}