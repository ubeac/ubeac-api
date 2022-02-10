namespace PhoneBook.Identity;

public static class Options
{
    public static string Environment;
    private const string ADMIN_USERNAME = "admin";
    private const string ADMIN_ROLE = "ADMIN";

    public static Action<UserOptions<AppUser>> User => options =>
    {
        if (Environment != "Testing")
        {
            var adminUser = new AppUser(ADMIN_USERNAME)
            {
                CustomProperty = "This is custom property of user"
            };
            options.AdminUser = adminUser;
            options.AdminPassword = "1qaz!QAZ";
            options.AdminRole = ADMIN_ROLE;
        }
    };

    public static Action<RoleOptions<AppRole>> Role => options =>
    {
        if (Environment != "Testing")
        {
            var adminRole = new AppRole("ADMIN")
            {
                CustomProperty = "This is custom property of role"
            };
            options.DefaultValues = new List<AppRole> { adminRole };
        }
    };

    public static Action<UnitOptions<AppUnit>> Unit => options =>
    {
        if (Environment != "Testing")
        {
            var mainBranch = new AppUnit
            {
                Name = "Main Branch",
                Code = "1",
                Type = "M",
                Description = "This is description",
                CustomProperty = "This is custom property of unit"
            };

            var subBranch = new AppUnit
            {
                Name = "Sub Branch",
                Code = "10",
                Type = "S",
                Description = "This is description",
                CustomProperty = "This is custom property of unit"
            };
            subBranch.SetParentUnit(mainBranch);

            options.DefaultValues = new List<AppUnit> { mainBranch, subBranch };
        }
    };

    public static Action<UnitTypeOptions<AppUnitType>> UnitType => options =>
    {
        if (Environment != "Testing")
        {
            var main = new AppUnitType
            {
                Name = "Main",
                Code = "M",
                Description = "This is description",
                CustomProperty = "This is custom property of unit type"
            };

            var sub = new AppUnitType
            {
                Name = "Sub",
                Code = "S",
                Description = "This is description",
                CustomProperty = "This is custom property of unit type"
            };

            options.DefaultValues = new List<AppUnitType> { main, sub };
        }
    };
}