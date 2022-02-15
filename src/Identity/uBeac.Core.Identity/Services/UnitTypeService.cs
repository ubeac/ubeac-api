using uBeac.Services;

namespace uBeac.Identity;

public class UnitTypeService<TKey, TUnitType> : EntityService<TKey, TUnitType>, IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeRepository<TKey, TUnitType> UnitTypeRepository;

    public UnitTypeService(IUnitTypeRepository<TKey, TUnitType> unitTypeRepository, IApplicationContext appContext) : base(unitTypeRepository, appContext)
    {
        UnitTypeRepository = unitTypeRepository;
    }

    public async Task<bool> Exists(string code, CancellationToken cancellationToken = default)
        => (await Repository.Find(unit => unit.Code == code, cancellationToken)).Any();
}

public class UnitTypeService<TUnitType> : UnitTypeService<Guid, TUnitType>, IUnitTypeService<TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeService(IUnitTypeRepository<TUnitType> unitTypeRepository, IApplicationContext appContext) : base(unitTypeRepository, appContext)
    {
    }
}