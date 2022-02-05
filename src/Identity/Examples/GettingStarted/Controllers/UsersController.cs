using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers;

public class UsersController : UsersControllerBase<User>
{
    public UsersController(IUserService<User> userService) : base(userService)
    {
    }
}