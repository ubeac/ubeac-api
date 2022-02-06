using Microsoft.AspNetCore.Authorization;

namespace PhoneBook.Identity;

[Authorize(Roles = "ADMIN")]
public class UsersController : UsersControllerBase<AppUser>
{
    public UsersController(IUserService<AppUser> userService) : base(userService)
    {
    }
}