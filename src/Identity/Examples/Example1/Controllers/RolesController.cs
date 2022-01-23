using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class RolesController : RolesControllerBase<Role>
    {
        public RolesController(IRoleService<Role> roleService) : base(roleService)
        {
        }
    }
}
