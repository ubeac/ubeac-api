using Microsoft.AspNetCore.Mvc;
using uBeac.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using uBeac.Logging.MongoDB;
using uBeac.Repositories.MongoDB;
using uBeac.Web;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files (IConfiguration)
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding logging
var logger = new LoggerConfiguration()
    .AddApiLogging()
    .WriteToMongoDB(builder.Configuration.GetConnectionString("LogConnection"))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Adding http logging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.MediaTypeOptions.AddText("application/json");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper();

// Disabling automatic model state validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Adding application context
builder.Services.AddApplicationContext();

// Adding debugger
builder.Services.AddDebugger();

// Adding swagger
builder.Services.AddCoreSwaggerWithJWT("Example");

// Adding mongodb
builder.Services.AddMongo<MongoDBContext>("DefaultConnection", builder.Environment.IsEnvironment("Testing"))
    .AddDefaultBsonSerializers();

// Adding history
builder.Services.AddHistory().UsingMongoDb().ForDefault();

// builder.Services.AddHistory().Using<MongoHistoryRepository<MongoDBContext>>()
//    .For<User>()
//    .For<Role>()
//    .For<Unit>()
//    .For<UnitType>()
//    .For<UnitRole>();

// Adding email provider
builder.Services.AddEmailProvider(builder.Configuration);

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
builder.Services.AddJwtService<User>(builder.Configuration);

// Adding authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

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
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCoreSwagger();
app.Run();

// For access test projects
public partial class Program { }