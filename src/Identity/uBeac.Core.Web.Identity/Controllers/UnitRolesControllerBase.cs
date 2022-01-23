using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity;

[Authorize(Roles = Roles.Admin)]
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
    public virtual async Task<IApiResult<bool>> Insert([FromBody] TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Insert(unitRole, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Replace(unitRole, cancellationToken);
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
            await UnitRoleService.Delete(id.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<TUnitRole>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var unitRoles = await UnitRoleService.GetAll(cancellationToken);
            return new ApiListResult<TUnitRole>(unitRoles);
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TUnitRole>();
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