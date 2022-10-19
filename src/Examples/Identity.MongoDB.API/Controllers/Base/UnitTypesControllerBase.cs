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
        await UnitTypeService.Create(unitType, cancellationToken);
        return unitType.Id.ToResult();
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TUnitType unitType, CancellationToken cancellationToken = default)
    {
        await UnitTypeService.Update(unitType, cancellationToken);
        return true.ToResult();
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Delete([FromBody] IdRequest<TKey> request, CancellationToken cancellationToken = default)
    {
        await UnitTypeService.Delete(request.Id, cancellationToken);
        return true.ToResult();
    }

    [HttpGet]
    public virtual async Task<IListResult<TUnitType>> GetAll(CancellationToken cancellationToken = default)
    {
        var unitTypes = await UnitTypeService.GetAll(cancellationToken);
        return unitTypes.ToListResult();
    }
}

public abstract class UnitTypesControllerBase<TUnitType> : UnitTypesControllerBase<Guid, TUnitType>
    where TUnitType : UnitType
{
    protected UnitTypesControllerBase(IUnitTypeService<TUnitType> unitTypeService) : base(unitTypeService)
    {
    }
}