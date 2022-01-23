using System.Threading.Tasks;
using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class AccountsController : AccountsControllerBase<User>
{
    public AccountsController(IUserService<User> userService) : base(userService)
    {
    }
}