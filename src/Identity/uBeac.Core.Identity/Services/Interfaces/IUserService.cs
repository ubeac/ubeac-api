﻿using uBeac.Services;

namespace uBeac.Identity;

public interface IUserService<TKey, TUser> : IService
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    Task Insert(TUser user, string password, CancellationToken cancellationToken = default);
    Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default);
    Task<TokenResult<TKey>> Authenticate(string username, string password, CancellationToken cancellationToken = default);
    Task<TKey> GetCurrentUserId(CancellationToken cancellationToken = default);
    Task ChangePassword(ChangePassword<TKey> changePassword, CancellationToken cancellationToken = default);
    Task ForgotPassword(string username, CancellationToken cancellationToken = default);
    Task RevokeTokens(TKey id, CancellationToken cancellationToken = default);
    Task<TokenResult<TKey>> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default);
    Task<TUser> GetById(TKey id, CancellationToken cancellationToken = default);
    Task Replace(TUser user, CancellationToken cancellationToken = default);
}

public interface IUserService<TUser> : IUserService<Guid, TUser>
    where TUser : User
{
}