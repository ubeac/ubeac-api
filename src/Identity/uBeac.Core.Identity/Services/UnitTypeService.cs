using uBeac.Services;

namespace uBeac.Identity;

public class UnitTypeService<TKey, TUnitType> : EntityService<TKey, TUnitType>, IUnitTypeService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeRepository<TKey, TUnitType> UnitTypeRepository;

    public UnitTypeService(IUnitTypeRepository<TKey, TUnitType> unitTypeRepository) : base(unitTypeRepository)
    {
        UnitTypeRepository = unitTypeRepository;
    }

    public async Task<bool> Exists(string code, CancellationToken cancellationToken = default)
        => (await Repository.Find(unit => unit.Code == code, cancellationToken)).Any();

    public async Task Insert(InsertUnitType unit, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnitType>();
        entity.Code = unit.Code;
        entity.Name = unit.Name;
        entity.Description = unit.Description;
        await Insert(entity, cancellationToken);
    }

    public async Task Replace(ReplaceUnitType<TKey> unit, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnitType>();
        entity.Id = unit.Id;
        entity.Code = unit.Code;
        entity.Name = unit.Name;
        entity.Description = unit.Description;
        await Replace(entity, cancellationToken);
    }
}

public class UnitTypeService<TUnitType> : UnitTypeService<Guid, TUnitType>, IUnitTypeService<TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeService(IUnitTypeRepository<TUnitType> unitTypeRepository) : base(unitTypeRepository)
    {
    }
}