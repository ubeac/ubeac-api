using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

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
    public virtual async Task<IResult<TRoleKey>> Create([FromBody] TRole role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Insert(role, cancellationToken);
            return role.Id.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<TRoleKey>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Update([FromBody] TRole role, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Update(role, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Delete([FromBody] IdRequest<TRoleKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            await RoleService.Delete(request.Id, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IListResult<TRole>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var roles = await RoleService.GetAll(cancellationToken);
            return roles.ToListResult();
        }
        catch (Exception ex)
        {
            return ex.ToListResult<TRole>();
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