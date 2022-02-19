using API;

public class UnitsController : UnitsControllerBase<Unit>
{
    public UnitsController(IUnitService<Unit> unitService) : base(unitService)
    {
    }
}