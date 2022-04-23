namespace API;

public class RolesController : RolesControllerBase<Role>
{
    public RolesController(IRoleService<Role> roleService) : base(roleService)
    {
    }
}