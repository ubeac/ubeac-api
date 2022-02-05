namespace PhoneBook.Identity;

public class AppUnitTypeService : UnitTypeService<AppUnitType>
{
    public AppUnitTypeService(IUnitTypeRepository<AppUnitType> unitTypeRepository) : base(unitTypeRepository)
    {
    }
}