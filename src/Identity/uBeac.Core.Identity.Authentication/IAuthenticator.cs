using System.Security.Claims;

namespace uBeac.Identity;

public interface IAuthenticator
{
    Task<IEnumerable<Claim>> Authenticate(string accessToken);
}