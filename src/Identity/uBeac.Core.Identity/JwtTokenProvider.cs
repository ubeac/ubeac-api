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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                    }
                ),
                Expires = DateTime.UtcNow.AddSeconds(_options.TokenExpiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                NotBefore = DateTime.UtcNow,
                IssuedAt = DateTime.UtcNow
            };

            var token = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
