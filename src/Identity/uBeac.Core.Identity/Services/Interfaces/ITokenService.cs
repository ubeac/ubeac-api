using System.Security.Claims;

namespace uBeac.Identity;

public interface ITokenService<TUserKey, TUser> 
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    Task<TokenResult> Generate(TUser user);
    Task<IEnumerable<Claim>> Validate(string accessToken);
}

public interface ITokenService<TUser> : ITokenService<Guid, TUser>
    where TUser : User
{
}