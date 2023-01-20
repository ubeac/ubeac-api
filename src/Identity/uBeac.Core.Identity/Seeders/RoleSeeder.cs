namespace uBeac.Identity.Seeders;

public class RoleSeeder<TKey, TRole> : IDataSeeder
    where TKey : IEquatable<TKey>
    where TRole : Role<TKey>
{
    private readonly IRoleService<TKey, TRole> _roleService;
    private readonly RoleOptions<TKey, TRole> _roleOptions;

    public RoleSeeder(IRoleService<TKey, TRole> roleService, RoleOptions<TKey, TRole> roleOptions)
    {
        _roleService = roleService;
        _roleOptions = roleOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var data = _roleOptions.DefaultValues?.ToList();

        if (data is null || data.Any() is false) return;

        foreach (var role in data)
        {
            if (await _roleService.Exists(role.Name, cancellationToken) is false)
            {
                await _roleService.Create(role, cancellationToken);
            }
        }
    }
}

public class RoleSeeder<TRole> : RoleSeeder<Guid, TRole>
    where TRole : Role
{
    public RoleSeeder(IRoleService<TRole> roleService, RoleOptions<TRole> roleOptions) : base(roleService, roleOptions)
    {
    }
}
