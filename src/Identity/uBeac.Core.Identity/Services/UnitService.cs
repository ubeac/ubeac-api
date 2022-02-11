using uBeac.Services;

namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : EntityService<TKey, TUnit>, IUnitService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    public UnitService(IUnitRepository<TKey, TUnit> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }

    public async Task<bool> Exists(string code, string type, CancellationToken cancellationToken = default)
        => (await Repository.Find(unit => unit.Code == code && unit.Type == type, cancellationToken)).Any();
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}