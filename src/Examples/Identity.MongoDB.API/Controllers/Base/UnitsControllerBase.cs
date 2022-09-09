using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

public abstract class UnitsController<TKey, TUnit> : BaseController
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitService<TKey, TUnit> UnitService;

    protected UnitsController(IUnitService<TKey, TUnit> unitService)
    {
        UnitService = unitService;
    }

    [HttpPost]
    public virtual async Task<IResult<TKey>> Create([FromBody] TUnit unit, CancellationToken cancellationToken = default)
    {
        await UnitService.Create(unit, cancellationToken);
        return unit.Id.ToResult();
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TUnit unit, CancellationToken cancellationToken = default)
    {
        await UnitService.Update(unit, cancellationToken);
        return true.ToResult();
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Delete([FromBody] IdRequest<TKey> request, CancellationToken cancellationToken = default)
    {
        await UnitService.Delete(request.Id, cancellationToken);
        return true.ToResult();
    }

    [HttpGet]
    public virtual async Task<IListResult<TUnit>> GetAll(CancellationToken cancellationToken = default)
    {
        var units = await UnitService.GetAll(cancellationToken);
        return units.ToListResult();
    }
}

public abstract class UnitsControllerBase<TUnit> : UnitsController<Guid, TUnit>
    where TUnit : Unit
{
    protected UnitsControllerBase(IUnitService<TUnit> unitService) : base(unitService)
    {
    }
}