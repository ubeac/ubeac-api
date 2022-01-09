using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    [Authorize(Roles = "admin")]
    public abstract class AccountControllerBase<TUserKey, TUser> : BaseController
       where TUserKey : IEquatable<TUserKey>
       where TUser : User<TUserKey>

    {
        protected readonly IUserService<TUserKey, TUser> UserService;

        public AccountControllerBase(IUserService<TUserKey, TUser> userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IApiResult<bool>> Register([FromBody] RegisterRequest model, CancellationToken cancellationToken = default)
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
        [HttpPost]
        public virtual async Task<IApiResult<TokenResult<TUserKey>>> Login([FromBody] LoginRequest model, CancellationToken cancellationToken = default)
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
        [HttpPost]
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

        [HttpGet]
        public virtual async Task<IApiResult<TUser>> Get(CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = await UserService.GetCurrentUserId(cancellationToken);
                if (userId == null)
                    throw new Exception("Unable to resolve current user!");

                var user = await UserService.GetById(userId, cancellationToken);
                if (user == null)
                    throw new Exception("Unable to resolve current user!");

                return user.ToApiResult();
            }
            catch (Exception ex)
            {
                return ex.ToApiResult<TUser>();
            }
        }

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

    public abstract class AccountControllerBase<TUser> : AccountControllerBase<Guid, TUser>
        where TUser : User
    {
        public AccountControllerBase(IUserService<TUser> userService) : base(userService)
        {
        }
    }

}
