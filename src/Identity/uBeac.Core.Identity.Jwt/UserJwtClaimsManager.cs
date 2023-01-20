using System.Security.Claims;

namespace uBeac.Identity.Jwt;

public class UserJwtClaimsManager<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    protected readonly IReadOnlyList<IUserJwtClaimsDecorator<TKey, TUser>> Decorators;

    public UserJwtClaimsManager(IEnumerable<IUserJwtClaimsDecorator<TKey, TUser>> decorators)
    {
        Decorators = decorators.ToList().AsReadOnly();
    }

    public virtual IReadOnlyList<Claim> GetClaims(TUser user)
    {
        var result = new List<Claim>();

        foreach (var decorator in Decorators)
        {
            var claims = decorator.GetClaims(user);

            result.AddRange(claims);
        }

        return result.AsReadOnly();
    }
}

public class UserJwtClaimsManager<TUser> : UserJwtClaimsManager<Guid, TUser>
    where TUser : User
{
    public UserJwtClaimsManager(IEnumerable<IUserJwtClaimsDecorator<TUser>> decorators) : base(decorators)
    {
    }
}
