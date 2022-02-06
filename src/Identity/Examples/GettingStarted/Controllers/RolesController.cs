using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class RolesController : RolesControllerBase<Role>
{
    public RolesController(IRoleService<Role> roleService) : base(roleService)
    {
    }
}
