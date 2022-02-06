using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class UsersController : UsersControllerBase<User>
{
    public UsersController(IUserService<User> userService) : base(userService)
    {
    }
}