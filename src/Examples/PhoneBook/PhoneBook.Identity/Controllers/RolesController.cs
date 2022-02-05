namespace PhoneBook.Identity;

public class RolesController : RolesControllerBase<AppRole>
{
    public RolesController(IRoleService<AppRole> roleService) : base(roleService)
    {
    }
}