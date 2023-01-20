using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace uBeac.Identity.Jwt;

public interface IUserJwtClaimsDecorator<TKey, in TUser>
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    IEnumerable<Claim> GetClaims(TUser user);
}

public interface IUserJwtClaimsDecorator<in TUser> : IUserJwtClaimsDecorator<Guid, TUser>
    where TUser : User
{
}

public class DefaultUserJwtClaimsDecorator<TKey, TUser> : IUserJwtClaimsDecorator<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    public IEnumerable<Claim> GetClaims(TUser user)
    {
        var userId = user.Id.ToString();

        var result = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToLongDateString()),
            new(JwtRegisteredClaimNames.Sub, userId),
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Name, user.UserName)
        };
        
        if (!string.IsNullOrWhiteSpace(user.NormalizedEmail))
        {
            result.Add(new Claim(ClaimTypes.Email, user.NormalizedEmail));
        }

        if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            result.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
        }

        result.AddRange(user.Roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        return result;
    }
}

public class DefaultUserJwtClaimsDecorator<TUser> : DefaultUserJwtClaimsDecorator<Guid, TUser>, IUserJwtClaimsDecorator<TUser>
    where TUser : User
{
}
