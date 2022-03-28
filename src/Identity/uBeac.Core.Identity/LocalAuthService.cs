using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using uBeac.Auth;

namespace uBeac.Identity;

public class LocalAuthService<TUserKey, TUser> : IAuthService
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    protected readonly IUserService<TUserKey, TUser> UserService;
    protected readonly IUserRoleService<TUserKey, TUser> UserRoleService;
    protected readonly IJwtTokenProvider TokenProvider;

    public LocalAuthService(IUserService<TUserKey, TUser> userService, IUserRoleService<TUserKey, TUser> userRoleService, IJwtTokenProvider tokenProvider)
    {
        UserService = userService;
        UserRoleService = userRoleService;
        TokenProvider = tokenProvider;
    }

    public virtual async Task<IEnumerable<Claim>> ValidateToken(string accessToken)
    {
        var principal = GetPrincipal(accessToken);

        var userId = GetUserId(principal);
        var user = await UserService.GetById(userId);
        if (user is null) return null;

        var tokenValidationResult = TokenProvider.ValidateToken(accessToken);
        if (tokenValidationResult is false) throw new Exception("Token is not valid.");

        return await GetClaims(user);
    }

    protected virtual ClaimsPrincipal GetPrincipal(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(accessToken);
        if (jwtToken == null) return null;
        var identity = new ClaimsIdentity(jwtToken.Claims);
        return new ClaimsPrincipal(identity);
    }

    protected virtual TUserKey GetUserId(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return (TUserKey)Convert.ChangeType(userId, typeof(TUserKey));
    }

    protected virtual async Task<IEnumerable<Claim>> GetClaims(TUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.NormalizedUserName)
        };
        if (!string.IsNullOrWhiteSpace(user.NormalizedEmail)) claims.Add(new Claim(ClaimTypes.Email, user.NormalizedEmail));
        if (!string.IsNullOrWhiteSpace(user.PhoneNumber)) claims.Add(new Claim(ClaimTypes.Email, user.PhoneNumber));

        var userRoles = await UserRoleService.GetRolesForUser(user.Id);
        var userRoleClaims = userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole));
        claims.AddRange(userRoleClaims);

        return claims;
    }
}

public class LocalAuthService<TUser> : LocalAuthService<Guid, TUser>
    where TUser : User
{
    public LocalAuthService(IUserService<TUser> userService, IUserRoleService<TUser> userRoleService, IJwtTokenProvider tokenProvider) : base(userService, userRoleService, tokenProvider)
    {
    }
}