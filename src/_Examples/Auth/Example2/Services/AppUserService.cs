using Microsoft.AspNetCore.Identity;
using uBeac.Identity;

namespace Example2.Services
{
    public class AppUserService : UserService<int, User<int>>
    {
        public AppUserService(UserManager<User<int>> userManager, IJwtTokenProvider jwtTokenProvider) : base(userManager, jwtTokenProvider)
        {
        }
    }
}
