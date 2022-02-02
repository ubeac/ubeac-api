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
    public virtual async Task<IApiResult<bool>> Insert([FromBody] InsertUnitTypeRequest unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Insert(new InsertUnitType
            {
                Code = unitType.Code,
                Name = unitType.Name,
                Description = unitType.Description
            }, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] ReplaceUnitTypeRequest<TKey> unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Replace(new ReplaceUnitType<TKey>
            {
                Id = unitType.Id,
                Code = unitType.Code,
                Name = unitType.Name,
                Description = unitType.Description
            }, cancellationToken);
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
    public virtual async Task<IApiListResult<UnitTypeViewModel<TKey>>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var unitTypes = await UnitTypeService.GetAll(cancellationToken);
            var unitTypesVm = unitTypes.Select(u => new UnitTypeViewModel<TKey>
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name,
                Description = u.Description
            });
            return unitTypesVm.ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<UnitTypeViewModel<TKey>>();
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