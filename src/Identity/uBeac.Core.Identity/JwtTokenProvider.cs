using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace uBeac.Identity
{
    public interface IJwtTokenProvider
    {
        string GenerateToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
        string GenerateRefreshToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
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

        public string GenerateToken<TKey, TUser>(TUser user)
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
            return new JwtSecurityTokenHandler().WriteToken(token);
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
            if (user.Roles?.Any() is true)
            {
                var claims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role));
                result.AddRange(claims);
            }
            return result;
        }
    }
}
