namespace API;

public class AccountsController : AccountsControllerBase<User>
{
    public AccountsController(IUserService<User> userService) : base(userService)
    {
    }
}