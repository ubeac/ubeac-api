namespace Identity
{
    public class AccountController : AccountControllerBase<User>
    {
        public AccountController(IUserService<User> userService) : base(userService)
        {
        }
    }
}

