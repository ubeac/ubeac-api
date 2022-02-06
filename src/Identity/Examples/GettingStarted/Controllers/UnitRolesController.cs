using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class UnitRolesController : UnitRolesControllerBase<UnitRole>
{
    public UnitRolesController(IUnitRoleService<UnitRole> unitRoleService) : base(unitRoleService)
    {
    }
}