using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Configuration.AddJsonConfig(builder.Environment);
builder.Services.AddCoreSwaggerWithJWT("Getting Started Example - Authentication");

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddMongo<MongoDBContext>("TestConnection", true);
}
else
{
    builder.Services.AddMongo<MongoDBContext>("DefaultConnection");
}

#region Identity

// Adding repositories
builder.Services.AddMongoDBUserRepository<MongoDBContext, User>();
builder.Services.AddMongoDBUserTokenRepository<MongoDBContext>();
builder.Services.AddMongoDBRoleRepository<MongoDBContext, Role>();
builder.Services.AddMongoDBUnitRepository<MongoDBContext, Unit>();
builder.Services.AddMongoDBUnitTypeRepository<MongoDBContext, UnitType>();
builder.Services.AddMongoDBUnitRoleRepository<MongoDBContext, UnitRole>();

// Adding services
builder.Services.AddUserService<UserService<User>, User>();
builder.Services.AddRoleService<RoleService<Role>, Role>();
builder.Services.AddUserRoleService<UserRoleService<User>, User>();
builder.Services.AddUnitService<UnitService<Unit>, Unit>();
builder.Services.AddUnitTypeService<UnitTypeService<UnitType>, UnitType>();
builder.Services.AddUnitRoleService<UnitRoleService<UnitRole>, UnitRole>();

// Adding Core Identity
builder.Services
    .AddIdentityUser<User>(configureOptions: options =>
    {
        var adminUser = new User("admin");
        options.AdminUser = adminUser;
        options.AdminPassword = "1qaz!QAZ";
        options.AdminRole = "ADMIN";
    })
    .AddIdentityRole<Role>(options =>
    {
        var adminRole = new Role("ADMIN");
        options.DefaultValues = new List<Role> { adminRole };
    })
    .AddIdentityUnit<Unit>(options =>
    {
        var headquarter = new Unit { Name = "Management Office", Code = "1000", Type = "HQ", Description = "Desc" };
        var tehranBranch = new Unit { Name = "Tehran Central Branch", Code = "4000", Type = "BH", Description = "Desc" };
        tehranBranch.SetParentUnit(headquarter);
        var mirdamadBranch = new Unit { Name = "Mirdamad Branch", Code = "40001", Type = "BR", Description = "Desc" };
        mirdamadBranch.SetParentUnit(tehranBranch);
        options.DefaultValues = new List<Unit> { headquarter, tehranBranch, mirdamadBranch };
    })
    .AddIdentityUnitType<UnitType>(options =>
    {
        var headquarter = new UnitType { Code = "HQ", Name = "Headquarter", Description = "Desc" };
        var firstBranch = new UnitType { Code = "FB", Name = "First Branch", Description = "Desc" };
        var secondBranch = new UnitType { Code = "SB", Name = "Second Branch", Description = "Desc" };
        options.DefaultValues = new List<UnitType> { headquarter, firstBranch, secondBranch };
    });

builder.Services.AddJwtAuthentication(builder.Configuration.GetInstance<JwtOptions>("Jwt"));

#endregion

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseCoreSwagger();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }