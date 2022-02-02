﻿using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity;

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
    public virtual async Task<IApiResult<bool>> Insert([FromBody] InsertUnitRoleRequest unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Insert(new InsertUnitRole
            {
                UserName = unitRole.UserName,
                UnitCode = unitRole.UnitCode,
                UnitType = unitRole.UnitType,
                Role = unitRole.Role
            }, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Replace([FromBody] ReplaceUnitRoleRequest<TKey> unitRole, CancellationToken cancellationToken = default)
    {
        try
        {
            await UnitRoleService.Replace(new ReplaceUnitRole<TKey>
            {
                Id = unitRole.Id,
                UserName = unitRole.UserName,
                UnitCode = unitRole.UnitCode,
                UnitType = unitRole.UnitType,
                Role = unitRole.Role
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