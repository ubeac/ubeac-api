using System.Security.Claims;

namespace uBeac.Identity;

public class LocalAuthenticator<TUserKey, TUser> : IAuthenticator
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    protected readonly ITokenService<TUserKey, TUser> _tokenService;

    public LocalAuthenticator(ITokenService<TUserKey, TUser> tokenService)
    {
        _tokenService = tokenService;
    }

    public virtual async Task<IEnumerable<Claim>> Authenticate(string accessToken)
        => await _tokenService.Validate(accessToken);
}

public class LocalAuthenticator<TUser> : LocalAuthenticator<Guid, TUser>
    where TUser : User
{
    public LocalAuthenticator(ITokenService<TUser> tokenService) : base(tokenService)
    {
    }
}