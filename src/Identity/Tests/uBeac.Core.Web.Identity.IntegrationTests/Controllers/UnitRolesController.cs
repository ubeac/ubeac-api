using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class UnitRolesController : UnitRolesControllerBase<UnitRole>
{
    public UnitRolesController(IUnitRoleService<UnitRole> unitRoleService) : base(unitRoleService)
    {
    }
}