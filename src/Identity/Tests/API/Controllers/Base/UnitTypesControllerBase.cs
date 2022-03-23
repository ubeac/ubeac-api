using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

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
    public virtual async Task<IResult<TKey>> Create([FromBody] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Create(unitType, cancellationToken);
            return unitType.Id.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<TKey>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitTypeService.Update(unitType, cancellationToken);
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
            await UnitTypeService.Delete(request.Id, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IListResult<TUnitType>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var unitTypes = await UnitTypeService.GetAll(cancellationToken);
            return unitTypes.ToListResult();
        }
        catch (Exception ex)
        {
            return ex.ToListResult<TUnitType>();
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