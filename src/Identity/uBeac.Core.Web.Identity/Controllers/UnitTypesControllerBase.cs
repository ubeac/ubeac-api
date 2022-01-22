using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity;

[Authorize(Roles = Roles.Admin)]
public abstract class UnitTypesControllerBase<TUnitTypeKey, TUnitType> : BaseController
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
{
    protected readonly IUnitTypeService<TUnitTypeKey, TUnitType> UnitTypeService;

    protected UnitTypesControllerBase(IUnitTypeService<TUnitTypeKey, TUnitType> unitTypeService)
    {
        UnitTypeService = unitTypeService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Create([FromBody, Required] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Insert(unitType, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPut]
    public virtual async Task<IApiResult<bool>> Update([FromBody, Required] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Update(unitType, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpDelete]
    public virtual async Task<IApiResult<bool>> Delete([FromBody, Required] TUnitTypeKey id, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Remove(id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<TUnitType>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var unitTypes = await UnitTypeService.GetAll(cancellationToken);
            return new ApiListResult<TUnitType>(unitTypes);
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TUnitType>();
        }
    }
}

public abstract class UnitTypesControllerBase<TUnitType> : UnitTypesControllerBase<Guid, TUnitType>
    where TUnitType : UnitType
{
    protected UnitTypesControllerBase(IUnitTypeService<TUnitType> unitTypeService) : base(unitTypeService)
    {
    }
}