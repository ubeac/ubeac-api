using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Identity;

public class AppUserService : UserService<AppUser>
{
    public AppUserService(UserManager<AppUser> userManager, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IUserTokenRepository userTokenRepository) : base(userManager, jwtTokenProvider, httpContextAccessor, jwtOptions, userTokenRepository)
    {
    }
}