namespace uBeac.Identity;

public class UnitTypeService<TKey, TUnitType> : IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeRepository<TKey, TUnitType> UnitTypeRepository;

    public UnitTypeService(IUnitTypeRepository<TKey, TUnitType> unitTypeRepository) 
    {
        UnitTypeRepository = unitTypeRepository;
    }
}

public class UnitTypeService<TUnitType> : UnitTypeService<Guid, TUnitType>, IUnitTypeService<TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeService(IUnitTypeRepository<TUnitType> unitTypeRepository) : base(unitTypeRepository)
    {
    }
}