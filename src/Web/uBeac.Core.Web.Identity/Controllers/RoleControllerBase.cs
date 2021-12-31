using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    //[Authorize(Roles = "admin")]
    [AllowAnonymous]
    public abstract class RoleControllerBase<TRoleKey, TRole> : BaseController
       where TRoleKey : IEquatable<TRoleKey>
       where TRole : Role<TRoleKey>
    {

        protected readonly IRoleService<TRoleKey, TRole> RoleService;

        public RoleControllerBase(IRoleService<TRoleKey, TRole> roleService)
        {
            RoleService = roleService;
        }

        [HttpPost]
        public virtual async Task<IApiResult<bool>> Add([FromBody][Required] TRole role, CancellationToken cancellationToken = default)
        {
            await RoleService.Insert(role, cancellationToken);

            return true.ToApiResult();
        }

        [HttpPost]
        public virtual async Task<IApiResult<bool>> Update([FromBody][Required] TRole role, CancellationToken cancellationToken = default)
        {
            await RoleService.Update(role, cancellationToken);

            return true.ToApiResult();
        }


        [HttpPost]
        public virtual async Task<IApiResult<bool>> Delete([FromBody] Entity<TRoleKey> role, CancellationToken cancellationToken = default)
        {
            await RoleService.Delete(role.Id, cancellationToken);

            return true.ToApiResult();
        }

        [HttpGet]
        public virtual async Task<IApiListResult<TRole>> GetAll(CancellationToken cancellationToken = default)
        {
            var roles = await RoleService.GetAll(cancellationToken);

            return new ApiListResult<TRole>(roles);
        }

    }

    public abstract class RoleControllerBase<TRole> : RoleControllerBase<Guid, TRole>
       where TRole : Role
    {
        protected RoleControllerBase(IRoleService<TRole> roleService) : base(roleService)
        {
        }
    }
}
