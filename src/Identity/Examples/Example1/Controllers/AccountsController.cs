using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class AccountsController : AccountsControllerBase<User>
    {
        public AccountsController(IUserService<User> userService) : base(userService)
        {
        }
    }
}
