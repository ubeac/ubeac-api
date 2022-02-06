using uBeac.Identity;
using uBeac.Web.Identity;

namespace GettingStarted;

public class UnitsController : UnitsControllerBase<Unit>
{
    public UnitsController(IUnitService<Unit> unitService) : base(unitService)
    {
    }
}