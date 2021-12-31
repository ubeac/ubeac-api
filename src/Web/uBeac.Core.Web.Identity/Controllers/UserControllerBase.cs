using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    [Authorize(Roles = "admin")]
    public abstract class UserControllerBase<TUserKey, TUser> : BaseController
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {

        protected readonly IUserService<TUserKey, TUser> UserService;

        public UserControllerBase(IUserService<TUserKey, TUser> userService)
        {
            UserService = userService;
        }

        //[HttpPost]
        //public virtual async Task<IApiResult<bool>> Add([FromBody][Required] TUser user, CancellationToken cancellationToken = default)
        //{
        //    await UserService.Insert(user, cancellationToken);

        //    return true.ToApiResult();
        //}

        //[HttpPost]
        //public virtual async Task<IApiResult<bool>> Update([FromBody][Required] TUser user, CancellationToken cancellationToken = default)
        //{
        //    await RoleService.Update(role, cancellationToken);

        //    return true.ToApiResult();
        //}


        //[HttpPost]
        //public virtual async Task<IApiResult<bool>> Delete([FromBody] Entity<TUserKey> id, CancellationToken cancellationToken = default)
        //{
        //    await RoleService.Delete(role.Id, cancellationToken);

        //    return true.ToApiResult();
        //}

        //[HttpGet]
        //public virtual async Task<IApiListResult<TRole>> GetAll(CancellationToken cancellationToken = default)
        //{
        //    var roles = await RoleService.GetAll(cancellationToken);

        //    return new ApiListResult<TRole>(roles);
        //}
    }
}
