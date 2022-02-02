using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity;

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
    public virtual async Task<IApiResult<bool>> Insert([FromBody] InsertUnitRequest unit, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Insert(new InsertUnit
            {
                Name = unit.Name,
                Code = unit.Code,
                Type = unit.Type,
                Description = unit.Description,
                ParentUnitId = unit.ParentUnitId
            }, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] ReplaceUnitRequest<TKey> unit, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitService.Replace(new ReplaceUnit<TKey>
            {
                Id = unit.Id,
                Name = unit.Name,
                Code = unit.Code,
                Type = unit.Type,
                Description = unit.Description,
                ParentUnitId = unit.ParentUnitId
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
            await UnitService.Delete(id.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<UnitViewModel<TKey>>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var units = await UnitService.GetAll(cancellationToken);
            var unitsVm = units.Select(u => new UnitViewModel<TKey>
            {
                Id = u.Id,
                Name = u.Name,
                Code = u.Code,
                Type = u.Type,
                Description = u.Description,
                ParentUnitId = u.ParentUnitId
            });
            return unitsVm.ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<UnitViewModel<TKey>>();
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