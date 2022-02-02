using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
    public virtual async Task<IApiResult<bool>> Insert([FromBody] InsertRoleRequest role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Insert(new InsertRole
            {
                Name = role.Name
            }, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] ReplaceRoleRequest<TRoleKey> role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Update(new ReplaceRole<TRoleKey>
            {
                Id = role.Id,
                Name = role.Name
            }, cancellationToken);
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
    public virtual async Task<IApiListResult<RoleViewModel<TRoleKey>>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var roles = await RoleService.GetAll(cancellationToken);
            var rolesVm = roles.Select(r => new RoleViewModel<TRoleKey>
            {
                Id = r.Id,
                Name = r.Name
            });
            return new ApiListResult<RoleViewModel<TRoleKey>>(rolesVm);
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<RoleViewModel<TRoleKey>>();
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

