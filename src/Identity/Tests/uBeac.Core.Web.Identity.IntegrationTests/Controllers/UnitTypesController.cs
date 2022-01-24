using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class UnitTypesController : UnitTypesControllerBase<UnitType>
{
    public UnitTypesController(IUnitTypeService<UnitType> unitTypeService) : base(unitTypeService)
    {
    }
}