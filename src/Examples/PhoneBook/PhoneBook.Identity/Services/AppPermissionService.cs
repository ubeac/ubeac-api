namespace PhoneBook.Identity;

public class AppPermissionService : PermissionService<AppPermission>
{
    public AppPermissionService(IPermissionRepository<AppPermission> repository) : base(repository)
    {
    }
}