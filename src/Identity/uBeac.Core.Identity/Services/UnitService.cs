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
}

public class UnitService<TUnit> : UnitService<Guid, TUnit>, IUnitService<TUnit>
    where TUnit : Unit
{
    public UnitService(IUnitRepository<TUnit> repository) : base(repository)
    {
    }
}