using System.Security.Claims;

namespace uBeac.Identity;

public class LocalAuthenticator<TUserKey, TUser> : IAuthenticator
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    protected readonly ITokenService<TUserKey, TUser> TokenService;
    protected readonly IUserService<TUserKey, TUser> UserService;

    public LocalAuthenticator(ITokenService<TUserKey, TUser> tokenService, IUserService<TUserKey, TUser> userService)
    {
        TokenService = tokenService;
        UserService = userService;
    }

    public virtual async Task<IEnumerable<Claim>> Authenticate(string accessToken)
    {
        var userId = await TokenService.Validate(accessToken);
        var user = await UserService.GetById(userId);
        return await UserService.GetClaims(user);
    }
}

public class LocalAuthenticator<TUser> : LocalAuthenticator<Guid, TUser>
    where TUser : User
{
    public LocalAuthenticator(ITokenService<TUser> tokenService, IUserService<TUser> userService) : base(tokenService, userService)
    {
    }
}