namespace uBeac.Identity
{
    public interface IUserService<TUserKey, TUser>
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        Task Insert(TUser user, string password, CancellationToken cancellationToken = default);
        Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default);
        Task<TokenResult<TUserKey>> Authenticate(string username, string password, CancellationToken cancellationToken = default);
        Task<TUser> GetById(TUserKey id, CancellationToken cancellationToken = default);
        Task<TUserKey> GetCurrentUserId(CancellationToken cancellationToken = default);
        Task ChangePassword(ChangePassword changePassword, CancellationToken cancellationToken = default);
        Task ForgotPassword(string username, CancellationToken cancellationToken = default);

    }

    public interface IUserService<TUser> : IUserService<Guid, TUser>
        where TUser : User
    {
    }
}
