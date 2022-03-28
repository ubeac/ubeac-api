using System.Security.Claims;

namespace uBeac.Auth;

public interface IAuthService
{
    Task<IEnumerable<Claim>> ValidateToken(string accessToken);
}