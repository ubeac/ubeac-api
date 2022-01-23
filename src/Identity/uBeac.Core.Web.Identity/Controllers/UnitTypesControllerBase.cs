using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity;

public abstract class UnitTypesControllerBase<TKey, TUnitType> : BaseController
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeService<TKey, TUnitType> UnitTypeService;

    protected UnitTypesControllerBase(IUnitTypeService<TKey, TUnitType> unitTypeService)
    {
        UnitTypeService = unitTypeService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Insert([FromBody] TUnitType unitType, CancellationToken cancellationToken = default)
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

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Replace(unitType, cancellationToken);
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
            await UnitTypeService.Delete(id.Id, cancellationToken);
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

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Delete([FromBody] IdRequest id, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Delete(id.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }
}