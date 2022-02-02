using uBeac.Repositories;
using uBeac.Services;

namespace uBeac.Identity;

public class UnitService<TKey, TUnit> : EntityService<TKey, TUnit>, IUnitService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    public UnitService(IUnitRepository<TKey, TUnit> repository) : base(repository)
    {
    }

    public async Task Insert(InsertUnit unit, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnit>();
        entity.Name = unit.Name;
        entity.Code = unit.Code;
        entity.Type = unit.Type;
        entity.Description = unit.Description;
        entity.ParentUnitId = unit.ParentUnitId;
        await Insert(entity, cancellationToken);
    }

    public async Task Replace(ReplaceUnit<TKey> unit, CancellationToken cancellationToken = default)
    {
        var entity = Activator.CreateInstance<TUnit>();
        entity.Id = unit.Id;
        entity.Name = unit.Name;
        entity.Code = unit.Code;
        entity.Type = unit.Type;
        entity.Description = unit.Description;
        entity.ParentUnitId = unit.ParentUnitId;
        await Replace(entity, cancellationToken);
    }

    public async Task<bool> Exists(string code, string type, CancellationToken cancellationToken = default)
        => (await Repository.Find(unit => unit.Code == code && unit.Type == type, cancellationToken)).Any();
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> repository) : base(repository)
    {
    }
}