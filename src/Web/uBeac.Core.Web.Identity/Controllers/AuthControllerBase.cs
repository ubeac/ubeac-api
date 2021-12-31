using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    [Authorize(Roles = "admin")]
    public abstract class AuthControllerBase<TUserKey, TUser> : BaseController
       where TUserKey : IEquatable<TUserKey>
       where TUser : User<TUserKey>

    {
        protected readonly IUserService<TUserKey, TUser> UserService;

        public AuthControllerBase(IUserService<TUserKey, TUser> userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IApiResult<bool>> Register([FromBody] RegisterRequest model, CancellationToken cancellationToken = default)
        {
            await UserService.Register(model.Username, model.Email, model.Password, cancellationToken);

            return true.ToApiResult();
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IApiResult<TokenResult<TUserKey>>> Login([FromBody] LoginRequest model, CancellationToken cancellationToken = default)
        {
            var authResult = await UserService.Authenticate(model.Username, model.Password, cancellationToken);
            return authResult.ToApiResult();
        }

        //[HttpGet]
        //public virtual async Task<IResultSet<TUserResponse>> Get(CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var user = await UserService.GetById(ApplicationContext.UserId, cancellationToken);
        //    var res = Mapper.Map<TUserResponse>(user);
        //    return res.ToResultSet();
        //}

        //[HttpPost]
        //public virtual async Task<IResultSet<bool>> Delete([FromBody][Required] TKey id, CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var isDeleted = await UserService.Delete(id, cancellationToken);
        //    return isDeleted.ToResultSet();
        //}

        //[HttpPost]
        //public virtual Task<IResultSet<bool>> Logout(CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    return Task.FromResult(true.ToResultSet());
        //}

        //[HttpPost]
        //public virtual async Task<IResultSet<bool>> ChangePassword([FromBody] ChangePasswordRequest model, CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var result = await UserService.ChangePassword(model, cancellationToken);
        //    return result.ToResultSet();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public virtual async Task<IResultSet<bool>> ForgotPassword([FromBody] ForgotPasswordRequest model, CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var result = await UserService.ChangePassword(model, cancellationToken);
        //    return result.ToResultSet();
        //}

    }

    public abstract class AuthControllerBase<TUser> : AuthControllerBase<Guid, TUser>
        where TUser : User
    {
        public AuthControllerBase(IUserService<TUser> userService) : base(userService)
        {
        }
    }

}
