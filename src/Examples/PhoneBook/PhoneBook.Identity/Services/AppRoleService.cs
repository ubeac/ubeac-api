using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Identity;

public class AppRoleService : RoleService<AppRole>
{
    public AppRoleService(RoleManager<AppRole> roleManager) : base(roleManager)
    {
    }
}