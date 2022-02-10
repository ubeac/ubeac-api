namespace PhoneBook.Identity;

// [Authorize(Roles = "ADMIN")]
public class RolesController : RolesControllerBase<AppRole>
{
    public RolesController(IRoleService<AppRole> roleService) : base(roleService)
    {
    }
}