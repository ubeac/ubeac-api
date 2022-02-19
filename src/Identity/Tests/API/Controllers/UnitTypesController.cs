namespace API;

public class UnitTypesController : UnitTypesControllerBase<UnitType>
{
    public UnitTypesController(IUnitTypeService<UnitType> unitTypeService) : base(unitTypeService)
    {
    }
}