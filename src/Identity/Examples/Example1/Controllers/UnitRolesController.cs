using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers;

public class UnitRolesController : UnitRolesControllerBase<UnitRole>
{
    public UnitRolesController(IUnitRoleService<UnitRole> unitRoleService) : base(unitRoleService)
    {
    }
}