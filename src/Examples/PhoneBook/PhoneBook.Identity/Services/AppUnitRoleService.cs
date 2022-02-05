namespace PhoneBook.Identity;

public class AppUnitRoleService : UnitRoleService<AppUnitRole>
{
    public AppUnitRoleService(IUnitRoleRepository<AppUnitRole> repository) : base(repository)
    {
    }
}