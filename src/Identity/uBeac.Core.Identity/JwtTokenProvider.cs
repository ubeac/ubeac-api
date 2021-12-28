using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace uBeac.Identity
{
    public interface IJwtTokenProvider
    {
        JwtSecurityToken GenerateToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
    }

    public class JwtTokenProvider : IJwtTokenProvider
    {

        private readonly JwtOptions _options;

        public JwtTokenProvider(JwtOptions options)
        {
            _options = options;
        }

        public JwtSecurityToken GenerateToken<TKey, TUser>(TUser user)
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
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    }
                ),
                Expires = DateTime.UtcNow.AddSeconds(_options.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            
            return jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        }
    }
}
