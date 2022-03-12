using uBeac.Services;

namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : EntityService<TKey, TUnit>, IUnitService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitRepository<TKey, TUnit> UnitRepository;

    public UnitService(IUnitRepository<TKey, TUnit> repository, IApplicationContext appContext) : base(repository, appContext)
    {
        UnitRepository = repository;
    }

    public virtual async Task<bool> Exists(string code, string type, CancellationToken cancellationToken = default)
        => (await Repository.Find(unit => unit.Code == code && unit.Type == type, cancellationToken)).Any();

    public virtual async Task<IEnumerable<TUnit>> GetByParentId(TKey parentUnitId, CancellationToken cancellationToken = default)
        => await UnitRepository.GetByParentId(parentUnitId, cancellationToken);
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> repository, IApplicationContext appContext) : base(repository, appContext)
    {
    }
}