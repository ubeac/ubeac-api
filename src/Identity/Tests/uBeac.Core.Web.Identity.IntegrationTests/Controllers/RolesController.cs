using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class RolesController : RolesControllerBase<Role>
{
    public RolesController(IRoleService<Role> roleService) : base(roleService)
    {
    }
}