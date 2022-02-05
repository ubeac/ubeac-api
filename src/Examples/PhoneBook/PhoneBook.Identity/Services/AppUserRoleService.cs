using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Identity;

public class AppUserRoleService : UserRoleService<AppUser>
{
    public AppUserRoleService(UserManager<AppUser> userManager) : base(userManager)
    {
    }
}