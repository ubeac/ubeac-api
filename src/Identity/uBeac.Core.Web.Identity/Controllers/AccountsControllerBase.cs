using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Identity.Consts;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    [Authorize(Roles = Roles.Admin)]
    public abstract class AccountsControllerBase<TUserKey, TUser> : BaseController
       where TUserKey : IEquatable<TUserKey>
       where TUser : User<TUserKey>
    {
        protected readonly IUserService<TUserKey, TUser> UserService;

        protected AccountsControllerBase(IUserService<TUserKey, TUser> userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public virtual async Task<IApiResult<bool>> Register(RegisterRequest model, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserService.Register(model.Username, model.Email, model.Password, cancellationToken);
                return true.ToApiResult();
            }
            catch (Exception ex)
            {
                return ex.ToApiResult<bool>();
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public virtual async Task<IApiResult<TokenResult<TUserKey>>> Login(LoginRequest model, CancellationToken cancellationToken = default)
        {
            try
            {
                var authResult = await UserService.Authenticate(model.Username, model.Password, cancellationToken);
                return authResult.ToApiResult();
            }
            catch (Exception ex)
            {
                return ex.ToApiResult<TokenResult<TUserKey>>();
            }
        }

        [AllowAnonymous]
        [HttpPut("RefreshToken")]
        public virtual async Task<IApiResult<TokenResult<TUserKey>>> RefreshToken(RefreshTokenRequest model, CancellationToken cancellationToken = default)
        {
            try
            {
                var authResult = await UserService.RefreshToken(model.RefreshToken, model.Token, cancellationToken);
                return authResult.ToApiResult();
            }
            catch (Exception ex)
            {
                return ex.ToApiResult<TokenResult<TUserKey>>();
            }
        }

    }

    public abstract class AccountControllerBase<TUser> : AccountsControllerBase<Guid, TUser>
        where TUser : User
    {
        protected AccountControllerBase(IUserService<TUser> userService) : base(userService)
        {
        }
    }

}
