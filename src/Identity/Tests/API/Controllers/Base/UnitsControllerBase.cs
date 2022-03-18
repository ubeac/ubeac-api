using System;
using System.Threading;
using System.Threading.Tasks;
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
        try
        {
            await UnitService.Create(unit, cancellationToken);
            return unit.Id.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<TKey>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TUnit unit, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Update(unit, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Delete([FromBody] IdRequest<TKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Delete(request.Id, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IListResult<TUnit>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var units = await UnitService.GetAll(cancellationToken);
            return units.ToListResult();
        }
        catch (Exception ex)
        {
            return ex.ToListResult<TUnit>();
        }
    }
}

public abstract class UnitsControllerBase<TUnit> : UnitsController<Guid, TUnit>
    where TUnit : Unit
{
    protected UnitsControllerBase(IUnitService<TUnit> unitService) : base(unitService)
    {
    }
}