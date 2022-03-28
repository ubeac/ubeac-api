using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace uBeac.Identity;

public interface IJwtTokenService<TUserKey, TUser> : ITokenService<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
}

public interface IJwtTokenService<TUser> : IJwtTokenService<Guid, TUser>, ITokenService<TUser>
    where TUser : User
{
}

public class JwtTokenService<TUserKey, TUser> : IJwtTokenService<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    protected readonly JwtOptions Options;
    protected readonly IUserService<TUserKey, TUser> UserService;
    protected readonly IUserRoleService<TUserKey, TUser> UserRoleService;

    public JwtTokenService(JwtOptions options, IUserService<TUserKey, TUser> userService, IUserRoleService<TUserKey, TUser> userRoleService)
    {
        Options = options;
        UserService = userService;
        UserRoleService = userRoleService;
    }

    public virtual async Task<TokenResult> Generate(TUser user)
    {
        return await Task.Run(() =>
        {
            var token = GenerateToken(user);
            var refreshToken = GenerateRefreshToken(user);
            return new TokenResult
            {
                AccessToken = token.Token,
                Expiry = token.Expiry,
                RefreshToken = refreshToken
            };
        });
    }

    protected virtual string GenerateRefreshToken(TUser user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    protected virtual JwtResult GenerateToken(TUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Options.Secret);
        var claims = GetJwtClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Options.Issuer,
            Audience = Options.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(Options.TokenExpiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow
        };

        var token = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return new JwtResult
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiry = tokenDescriptor.Expires.Value
        };
    }

    protected virtual List<Claim> GetJwtClaims(TUser user)
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
        if (user.Email != null) result.Add(new Claim(ClaimTypes.Email, user.Email));
        return result;
    }

    public virtual async Task<IEnumerable<Claim>> Validate(string accessToken)
    {
        var principal = GetPrincipal(accessToken);

        var userId = GetUserId(principal);
        var user = await UserService.GetById(userId);
        if (user is null) return null;

        var tokenValidationResult = ValidateToken(accessToken);
        if (tokenValidationResult is false) throw new Exception("Token is not valid.");

        return await GetUserClaims(user);
    }

    protected virtual async Task<IEnumerable<Claim>> GetUserClaims(TUser user)
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

    protected virtual bool ValidateToken(string accessToken)
    {
        if (accessToken == null) return false;

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Options.Secret);

        try
        {
            jwtTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                SaveSigninToken = true,
                ValidAudience = Options.Audience,
                ValidIssuer = Options.Issuer
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class JwtTokenService<TUser> : JwtTokenService<Guid, TUser>, IJwtTokenService<TUser>
    where TUser : User
{
    public JwtTokenService(JwtOptions options, IUserService<TUser> userService, IUserRoleService<TUser> userRoleService) : base(options, userService, userRoleService)
    {
    }
}