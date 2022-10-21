namespace API;

public class UnitRolesController : UnitRolesControllerBase<UnitRole>
{
    public UnitRolesController(IUnitRoleService<UnitRole> unitRoleService) : base(unitRoleService)
    {
    }
}