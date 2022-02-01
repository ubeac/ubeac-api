using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class UsersController : UsersControllerBase<User>
{
    public UsersController(IUserService<User> userService) : base(userService)
    {
    }
}