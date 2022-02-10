namespace PhoneBook.Identity;

public class UnitTypesController : UnitTypesControllerBase<AppUnitType>
{
    public UnitTypesController(IUnitTypeService<AppUnitType> unitTypeService) : base(unitTypeService)
    {
    }
}