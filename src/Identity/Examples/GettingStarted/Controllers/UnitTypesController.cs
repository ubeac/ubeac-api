using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers;

public class UnitTypesController : UnitTypesControllerBase<UnitType>
{
    public UnitTypesController(IUnitTypeService<UnitType> unitTypeService) : base(unitTypeService)
    {
    }
}