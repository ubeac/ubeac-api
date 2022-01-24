using uBeac.Identity;

namespace uBeac.Web.Identity.IntegrationTests;

public class UnitsController : UnitsControllerBase<Unit>
{
    public UnitsController(IUnitService<Unit> unitService) : base(unitService)
    {
    }
}