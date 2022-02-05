using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity;

public abstract class RolesControllerBase<TRoleKey, TRole> : BaseController
   where TRoleKey : IEquatable<TRoleKey>
   where TRole : Role<TRoleKey>
{
    protected readonly IRoleService<TRoleKey, TRole> RoleService;

    protected RolesControllerBase(IRoleService<TRoleKey, TRole> roleService)
    {
        RoleService = roleService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Insert([FromBody] TRole role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Insert(role, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] TRole role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Update(role, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Delete([FromBody] IdRequest<TRoleKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Delete(request.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<TRole>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var roles = await RoleService.GetAll(cancellationToken);
            return roles.ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TRole>();
        }
    }
}

public abstract class RolesControllerBase<TRole> : RolesControllerBase<Guid, TRole>
   where TRole : Role
{
    protected RolesControllerBase(IRoleService<TRole> roleService) : base(roleService)
    {
    }
}

