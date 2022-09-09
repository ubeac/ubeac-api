using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

public abstract class UnitRolesControllerBase<TKey, TUnitRole> : BaseController
    where TKey : IEquatable<TKey>
    where TUnitRole : UnitRole<TKey>
{
    protected readonly IUnitRoleService<TKey, TUnitRole> UnitRoleService;

    protected UnitRolesControllerBase(IUnitRoleService<TKey, TUnitRole> unitRoleService)
    {
        UnitRoleService = unitRoleService;
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Create([FromBody] TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Create(unitRole, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Update(unitRole, cancellationToken);
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
            await UnitRoleService.Delete(request.Id, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IListResult<TUnitRole>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var unitRoles = await UnitRoleService.GetAll(cancellationToken);
            return unitRoles.ToListResult();
        }
        catch (Exception ex)
        {
            return ex.ToListResult<TUnitRole>();
        }
    }
}

public abstract class UnitRolesControllerBase<TUnitRole> : UnitRolesControllerBase<Guid, TUnitRole>
    where TUnitRole : UnitRole
{
    protected UnitRolesControllerBase(IUnitRoleService<TUnitRole> unitRoleService) : base(unitRoleService)
    {
    }
}