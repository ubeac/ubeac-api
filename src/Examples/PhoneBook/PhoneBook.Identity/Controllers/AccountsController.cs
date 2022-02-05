namespace PhoneBook.Identity;

public class AccountsController : AccountsControllerBase<AppUser>
{
    public AccountsController(IUserService<AppUser> userService) : base(userService)
    {
    }
}