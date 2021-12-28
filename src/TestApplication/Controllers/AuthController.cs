using AutoMapper;
using uBeac.Identity;
using uBeac.Web.Identity;

namespace TestApplication.Controllers
{
    public class AuthController : AuthControllerBase<Guid, User<Guid>, RegisterRequest, LoginRequest, LoginResponse<Guid>>
    {
        public AuthController(IUserService<Guid, User<Guid>> userService, IMapper mapper) : base(userService, mapper)
        {
        }
    }
}
