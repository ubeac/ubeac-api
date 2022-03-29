using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using uBeac.Repositories.MongoDB;
using uBeac.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Adding json config files
builder.Configuration.AddJsonConfig(builder.Environment);

// Get options from configuration files
var emailOptions = builder.Configuration.GetInstance<EmailProviderOptions>("Email");
var jwtOptions = builder.Configuration.GetInstance<JwtOptions>("Jwt");

// Disabling automatic model state validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Adding debugger
builder.Services.AddDebugger();

// Adding swagger
builder.Services.AddCoreSwaggerWithJWT("Example");

// Adding mongodb
builder.Services.AddMongo<MongoDBContext>("DefaultConnection", builder.Environment.IsEnvironment("Testing"))
    .AddDefaultBsonSerializers();

// Adding application context
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();

// Adding email provider
builder.Services.AddEmailProvider(emailOptions);

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

// Adding jwt provider
builder.Services.AddJwtService<User>(jwtOptions);

// Adding authentication
builder.Services.AddAuthentication<LocalAuthenticator<User>>(new AuthenticationOptions
{
    Issuer = jwtOptions.Issuer,
    Audience = jwtOptions.Audience,
    Secret = jwtOptions.Secret
});

// Adding identity core
builder.Services
    .AddIdentityUser<User>(configureOptions: options =>
    {
        options.AdminUser = new User("admin");
        options.AdminPassword = "1qaz!QAZ";
        options.AdminRole = "ADMIN";
    })
    .AddIdentityRole<Role>(configureOptions: options =>
    {
        options.DefaultValues = new List<Role> { new("ADMIN") };
    })
    .AddIdentityUnit<Unit>(configureOptions: options =>
    {
        var firstUnit = new Unit { Code = "1", Name = "First", Type = "A" };
        var secondUnit = new Unit { Code = "2", Name = "Second", Type = "B" };
        secondUnit.SetParentUnit(firstUnit);

        options.DefaultValues = new List<Unit> { firstUnit, secondUnit };
    })
    .AddIdentityUnitType<UnitType>(configureOptions: options =>
    {
        options.DefaultValues = new List<UnitType>
        {
            new() { Code = "A", Name = "First" },
            new() { Code = "B", Name = "Second" }
        };
    });

var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseMiddleware<AuthenticationMiddleware>(); // This middleware should be called after UseAuthentication
app.UseAuthorization();
app.MapControllers();
app.UseCoreSwagger();
app.Run();

// For access test projects
public partial class Program { }