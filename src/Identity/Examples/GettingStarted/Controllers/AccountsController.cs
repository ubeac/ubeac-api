using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class AccountsController : AccountsControllerBase<User>
{
    public AccountsController(IUserService<User> userService) : base(userService)
    {
    }
}
