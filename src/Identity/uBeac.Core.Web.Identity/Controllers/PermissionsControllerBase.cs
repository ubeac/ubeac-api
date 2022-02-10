using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity;

public abstract class PermissionsControllerBase<TKey, TPermission> : BaseController
    where TKey : IEquatable<TKey>
    where TPermission : Permission<TKey>
{
    protected readonly IPermissionService<TKey, TPermission> PermissionService;

    protected PermissionsControllerBase(IPermissionService<TKey, TPermission> permissionService)
    {
        PermissionService = permissionService;
    }

    [HttpPost]
    public virtual async Task<IApiResult<TKey>> Insert([FromBody] TPermission permission, CancellationToken cancellationToken = default)
    {
        try
        {
            await PermissionService.Insert(permission, cancellationToken);
            return permission.Id.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<TKey>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] TPermission permission, CancellationToken cancellationToken = default)
    {
        try
        {
            await PermissionService.Replace(permission, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Delete([FromBody] IdRequest<TKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            await PermissionService.Delete(request.Id, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<TPermission>> All(CancellationToken cancellationToken = default)
    {
        try
        {
            var permissions = await PermissionService.GetAll(cancellationToken);
            return permissions.ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<TPermission>();
        }
    }
}

public abstract class PermissionsControllerBase<TPermission> : PermissionsControllerBase<Guid, TPermission>
    where TPermission : Permission
{
    protected PermissionsControllerBase(IPermissionService<TPermission> permissionService) : base(permissionService)
    {
    }
}