using uBeac.Services;

namespace uBeac.Identity
{
    public interface IUserService<TUserKey, TUser>
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        Task Insert(InsertUser user, string password, CancellationToken cancellationToken = default);
        Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default);
        Task<TokenResult<TUserKey>> Authenticate(string username, string password, CancellationToken cancellationToken = default);
        Task<TUserKey> GetCurrentUserId(CancellationToken cancellationToken = default);
        Task ChangePassword(ChangePassword<TUserKey> changePassword, CancellationToken cancellationToken = default);
        Task ForgotPassword(string username, CancellationToken cancellationToken = default);
        Task RevokeTokens(TUserKey id, CancellationToken cancellationToken = default);
        Task<TokenResult<TUserKey>> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default);
        Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default);
        Task<TUser> GetById(TUserKey id, CancellationToken cancellationToken = default);
        Task Replace(TUser user, CancellationToken cancellationToken = default);
    }

    public interface IUserService<TUser> : IUserService<Guid, TUser>
        where TUser : User
    {
    }
}
