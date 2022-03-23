﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

public abstract class AccountsControllerBase<TUserKey, TUser> : BaseController
   where TUserKey : IEquatable<TUserKey>
   where TUser : User<TUserKey>
{
    protected readonly IUserService<TUserKey, TUser> UserService;

    protected AccountsControllerBase(IUserService<TUserKey, TUser> userService)
    {
        UserService = userService;
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> Register([FromBody] RegisterRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            await UserService.Register(model.UserName, model.Email, model.Password, cancellationToken);
            return true.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<TokenResult<TUserKey>>> Login([FromBody] LoginRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            var authResult = await UserService.Authenticate(model.UserName, model.Password, cancellationToken);
            return authResult.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<TokenResult<TUserKey>>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<TokenResult<TUserKey>>> RefreshToken([FromBody] RefreshTokenRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            var authResult = await UserService.RefreshToken(model.RefreshToken, model.Token, cancellationToken);
            return authResult.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<TokenResult<TUserKey>>();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> ForgotPassword([FromBody] ForgotPasswordRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            await UserService.ForgotPassword(model.UserName, cancellationToken);
            return true.ToResult();

        }
        catch (Exception)
        {
            return false.ToResult();
        }
    }

    [HttpPost]
    public virtual async Task<IResult<bool>> ResetPassword([FromBody] ResetPasswordRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            await UserService.ResetPassword(model.UserName, model.Token, model.NewPassword, cancellationToken);
            return true.ToResult();
        }
        catch (Exception)
        {
            return false.ToResult();
        }
    }
}

public abstract class AccountsControllerBase<TUser> : AccountsControllerBase<Guid, TUser>
    where TUser : User
{
    protected AccountsControllerBase(IUserService<TUser> userService) : base(userService)
    {
    }
}
