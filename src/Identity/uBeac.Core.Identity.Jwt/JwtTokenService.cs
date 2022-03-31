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

    public JwtTokenService(Microsoft.Extensions.Options.IOptions<JwtOptions> options)
    {
        Options = options.Value;
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

    public virtual async Task<TUserKey> Validate(string accessToken)
    {
        return await Task.Run(() =>
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Options.Secret);
            var principal = jwtTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                SaveSigninToken = true,
                ValidAudience = Options.Audience,
                ValidIssuer = Options.Issuer
            }, out _);

            var userId = GetUserId(principal);
            if (userId is null) throw new Exception("Token is not valid.");
            return userId;
        });
    }

    public virtual async Task<TUserKey> ValidateExpiredToken(string accessToken)
    {
        return await Task.Run(() =>
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Options.Secret);
            var principal = jwtTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = false,
                SaveSigninToken = true,
                ValidAudience = Options.Audience,
                ValidIssuer = Options.Issuer
            }, out _);

            var userId = GetUserId(principal);
            if (userId is null) throw new Exception("Token is not valid.");
            return userId;
        });
    }

    protected virtual TUserKey GetUserId(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier) ??
                     principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return userId.GetTypedKey<TUserKey>();
    }
}

public class JwtTokenService<TUser> : JwtTokenService<Guid, TUser>, IJwtTokenService<TUser>
    where TUser : User
{
    public JwtTokenService(Microsoft.Extensions.Options.IOptions<JwtOptions> options) : base(options)
    {
    }
}