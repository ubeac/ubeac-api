using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    public abstract class AuthControllerBase<TUserKey, TUser, TRegisterRequest, TLoginRequest, TLoginResponse> : BaseController
       where TUserKey : IEquatable<TUserKey>
       where TUser : User<TUserKey>
       where TRegisterRequest : RegisterRequest
       where TLoginRequest : LoginRequest
       where TLoginResponse : LoginResponse<TUserKey>

    {
        protected readonly IUserService<TUserKey, TUser> UserService;
        protected readonly IMapper Mapper;

        protected AuthControllerBase(IUserService<TUserKey, TUser> userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IResultSet<TLoginResponse>> Register([FromBody] TRegisterRequest model, CancellationToken cancellationToken = default)
        {
            var user = Mapper.Map<TUser>(model);

            await UserService.Create(user, model.Password, cancellationToken);

            var authResult = await UserService.Authenticate(user.UserName, model.Password, cancellationToken);
            var x = Mapper.Map<TLoginResponse>(authResult);
            return new ResultSet<TLoginResponse>(x);
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IResultSet<TLoginResponse>> Login([FromBody] TLoginRequest model, CancellationToken cancellationToken = default)
        {
            var authResult = await UserService.Authenticate(model.Username, model.Password, cancellationToken);
            var response = Mapper.Map<TLoginResponse>(authResult);
            return response.ToResultSet();
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
}
