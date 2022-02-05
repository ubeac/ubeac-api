namespace PhoneBook.Identity;

public class AppUnitService : UnitService<AppUnit>
{
    public AppUnitService(IUnitRepository<AppUnit> repository) : base(repository)
    {
    }
}