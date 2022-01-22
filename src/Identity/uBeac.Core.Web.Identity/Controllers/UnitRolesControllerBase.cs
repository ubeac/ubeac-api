using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity;

[Authorize(Roles = Roles.Admin)]
public abstract class UnitRolesControllerBase<TUnitRoleKey, TUnitRole> : BaseController
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
    protected readonly IUnitRoleService<TUnitRoleKey, TUnitRole> UnitRoleService;

    protected UnitRolesControllerBase(IUnitRoleService<TUnitRoleKey, TUnitRole> unitRoleService)
    {
        UnitRoleService = unitRoleService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Create([FromBody, Required] TUnitRole unitRole, CancellationToken cancellationToken = default)
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

    [HttpPut]
    public virtual async Task<IApiResult<bool>> Update([FromBody, Required] TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Update(unitRole, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpDelete]
    public virtual async Task<IApiResult<bool>> Delete([FromBody, Required] TUnitRoleKey id, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Remove(id, cancellationToken);
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