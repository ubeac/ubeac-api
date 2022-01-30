using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity;

// TODO: this shouldn't be hardcoded. The admin role name should be defined in stratup config
[Authorize(Roles = Roles.Admin)]
public abstract class UnitsControllerBase<TKey, TUnit> : BaseController
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitService<TKey, TUnit> UnitService;

    protected UnitsControllerBase(IUnitService<TKey, TUnit> unitService)
    {
        UnitService = unitService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Insert([FromBody] TUnit unit, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Insert(unit, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] TUnit unit, CancellationToken cancellationToken = default)
    {
        try
        {
            unit = await UnitService.Replace(unit, cancellationToken);
            if (unit == null) throw new NullReferenceException("Unit not found");
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Delete([FromBody] IdRequest<TKey> id, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Delete(id.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<TUnit>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var units = await UnitService.GetAll(cancellationToken);
            return units.ToList().ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TUnit>();
        }
    }
}

public abstract class UnitsControllerBase<TUnit> : UnitsControllerBase<Guid, TUnit>
    where TUnit : Unit
{
    protected UnitsControllerBase(IUnitService<TUnit> unitService) : base(unitService)
    {
    }
}