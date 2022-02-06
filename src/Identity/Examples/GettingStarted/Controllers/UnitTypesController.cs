using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class UnitTypesController : UnitTypesControllerBase<UnitType>
{
    public UnitTypesController(IUnitTypeService<UnitType> unitTypeService) : base(unitTypeService)
    {
    }
}