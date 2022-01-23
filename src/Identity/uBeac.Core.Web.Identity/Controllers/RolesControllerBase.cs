﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    [Authorize(Roles = Roles.Admin)]
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
        public virtual async Task<IApiResult<bool>> Create([FromBody, Required] TRole role, CancellationToken cancellationToken = default)
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

        [HttpPut]
        public virtual async Task<IApiResult<bool>> Update([FromBody, Required] TRole role, CancellationToken cancellationToken = default)
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

        [HttpDelete]
        public virtual async Task<IApiResult<bool>> Delete([FromBody, Required] TRoleKey id, CancellationToken cancellationToken = default)
        {
            try
            {
                await RoleService.Delete(id, cancellationToken);
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
                return new ApiListResult<TRole>(roles);
            }
            catch (Exception ex)
            {
                return ex.ToApiListResult<TRole>();
            }
        }
    }

    public abstract class RoleControllerBase<TRole> : RolesControllerBase<Guid, TRole>
       where TRole : Role
    {
        protected RoleControllerBase(IRoleService<TRole> roleService) : base(roleService)
        {
        }
    }
}