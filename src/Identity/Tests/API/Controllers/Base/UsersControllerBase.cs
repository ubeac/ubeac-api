﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace API;

public abstract class UsersControllerBase<TUserKey, TUser> : BaseController
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    protected readonly IUserService<TUserKey, TUser> UserService;
    protected readonly IMapper Mapper;

    protected UsersControllerBase(IUserService<TUserKey, TUser> userService, IMapper mapper)
    {
        UserService = userService;
        Mapper = mapper;
    }

    [HttpPost]
    public virtual async Task<IApiResult<TUserKey>> Create([FromBody] InsertUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = Mapper.Map<TUser>(request);
            await UserService.Insert(user, request.Password, cancellationToken);
            return user.Id.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<TUserKey>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> Update([FromBody] ReplaceUserRequest<TUserKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await UserService.GetById(request.Id, cancellationToken);
            user.PhoneNumber = request.PhoneNumber;
            user.PhoneNumberConfirmed = request.PhoneNumberConfirmed;
            user.Email = request.Email;
            user.EmailConfirmed = request.EmailConfirmed;
            user.LockoutEnabled = request.LockoutEnabled;
            user.LockoutEnd = request.LockoutEnd;
            await UserService.Replace(user, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public virtual async Task<IApiResult<bool>> ChangePassword([FromBody] ChangePasswordRequest<TUserKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            await UserService.ChangePassword(new ChangePassword<TUserKey>
            {
                UserId = request.UserId,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            }, cancellationToken);
            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiResult<UserResponse<TUserKey>>> GetById([FromQuery] IdRequest<TUserKey> request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await UserService.GetById(request.Id, cancellationToken);
            var userVm = new UserResponse<TUserKey>
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
            return userVm.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<UserResponse<TUserKey>>();
        }
    }

    [HttpGet]
    public virtual async Task<IApiListResult<UserResponse<TUserKey>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var users = await UserService.GetAll(cancellationToken);
            var usersVm = users.Select(u => new UserResponse<TUserKey>
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed
            });
            return usersVm.ToApiListResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiListResult<UserResponse<TUserKey>>();
        }
    }
}

public abstract class UsersControllerBase<TUser> : UsersControllerBase<Guid, TUser>
    where TUser : User
{
    protected UsersControllerBase(IUserService<TUser> userService, IMapper mapper) : base(userService, mapper)
    {
    }
}