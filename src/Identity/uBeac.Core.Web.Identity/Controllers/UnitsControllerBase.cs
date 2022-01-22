using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity;

[Authorize(Roles = Roles.Admin)]
public abstract class UnitsControllerBase<TUnitKey, TUnit> : BaseController
    where TUnitKey : IEquatable<TUnitKey>
    where TUnit : Unit<TUnitKey>
{
    protected readonly IUnitService<TUnitKey, TUnit> UnitService;

    protected UnitsControllerBase(IUnitService<TUnitKey, TUnit> unitService)
    {
        UnitService = unitService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Create([FromBody, Required] TUnit unit, CancellationToken cancellationToken = default)
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

    [HttpPut]
    public virtual async Task<IApiResult<bool>> Update([FromBody, Required] TUnit unit, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Update(unit, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpDelete]
    public virtual async Task<IApiResult<bool>> Delete([FromBody, Required] TUnitKey id, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Remove(id, cancellationToken);
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
            return new ApiListResult<TUnit>(units);
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TUnit>();
        }
    }
}

public abstract class UnitControllerBase<TUnit> : UnitsControllerBase<Guid, TUnit>
    where TUnit : Unit
{
    protected UnitControllerBase(IUnitService<TUnit> unitService) : base(unitService)
    {
    }
}