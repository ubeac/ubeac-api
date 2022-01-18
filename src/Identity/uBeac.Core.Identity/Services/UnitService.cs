namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : IUnitService<TKey, TUnit> 
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitRepository<TKey, TUnit> UnitRepository;

    public UnitService(IUnitRepository<TKey, TUnit> unitRepository)
    {
        UnitRepository = unitRepository;
    }

    public async Task Insert(TUnit unit, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await UnitRepository.Insert(unit, cancellationToken);
    }
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> unitRepository) : base(unitRepository)
    {
    }
}