using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace uBeac.Identity;

public interface IJwtTokenProvider
{
    GenerateTokenResult GenerateToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
    string GenerateRefreshToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
    bool ValidateToken(string token);
}

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtOptions _options;

    public JwtTokenProvider(JwtOptions options)
    {
        _options = options;
    }

    public string GenerateRefreshToken<TKey, TUser>(TUser user)
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public bool ValidateToken(string token)
    {
        if (token == null) return false;

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);

        try
        {
            jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                SaveSigninToken = true,
                ValidAudience = _options.Audience,
                ValidIssuer = _options.Issuer
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public GenerateTokenResult GenerateToken<TKey, TUser>(TUser user)
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);
        var claims = GetClaims<TKey, TUser>(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_options.TokenExpiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow
        };

        var token = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return new GenerateTokenResult
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiry = tokenDescriptor.Expires.Value
        };
    }

    private List<Claim> GetClaims<TKey, TUser>(TUser user)
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
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
}