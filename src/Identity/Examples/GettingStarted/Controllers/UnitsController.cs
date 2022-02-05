using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers;

public class UnitsController : UnitsControllerBase<Unit>
{
    public UnitsController(IUnitService<Unit> unitService) : base(unitService)
    {
    }
}