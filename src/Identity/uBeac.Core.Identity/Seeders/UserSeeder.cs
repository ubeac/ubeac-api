namespace uBeac.Identity.Seeders;

public class UserSeeder<TKey, TUser> : IDataSeeder
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    private readonly IUserService<TKey, TUser> _userService;
    private readonly IUserRoleService<TKey, TUser> _userRoleService;
    private readonly UserOptions<TKey, TUser> _userOptions;

    public UserSeeder(IUserService<TKey, TUser> userService, IUserRoleService<TKey, TUser> userRoleService, UserOptions<TKey, TUser> userOptions)
    {
        _userService = userService;
        _userRoleService = userRoleService;
        _userOptions = userOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var adminUser = _userOptions.AdminUser;
        var adminPassword = _userOptions.AdminPassword;

        await CreateAdminUser(adminUser, adminPassword, cancellationToken);
    }

    public async Task CreateAdminUser(TUser user, string password, CancellationToken cancellationToken = default)
    {
        var role = _userOptions.AdminRole;

        if (user is null || password is null || role is null) return;

        if (await _userService.ExistsUserName(user.UserName, cancellationToken) is false)
        {
            await _userService.Create(user, password, cancellationToken);
            await _userRoleService.AddRoles(user.Id, new List<string> { role }, cancellationToken);
        }
    }
}

public class UserSeeder<TUser> : UserSeeder<Guid, TUser>
    where TUser : User
{
    public UserSeeder(IUserService<TUser> userService, IUserRoleService<TUser> userRoleService, UserOptions<TUser> userOptions) : base(userService, userRoleService, userOptions)
    {
    }
}
