using System.Reflection;
using API;
using Microsoft.AspNetCore.Mvc;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History.MongoDB;
using uBeac.Repositories.MongoDB;
using uBeac.Web;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files (IConfiguration)
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding bson serializers
//builder.Services.AddDefaultBsonSerializers();

// Adding http logging
builder.Services.AddMongoDbHttpLogging(builder.Configuration.GetInstance<MongoDbHttpLogOptions>("HttpLogging"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
builder.Services.AddMongo<MongoDBContext>("DefaultConnection");

// Adding sql server
builder.Services.AddSqlServerDatabase<EFIdentityDbContext>(builder.Configuration.GetConnectionString("SqlServer"));

// Adding history
builder.Services.AddHistory<MongoDBHistoryRepository>().For<User>().Register();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("History"));

// Adding email provider
builder.Services.AddEmailProvider(builder.Configuration);

// Adding repositories
// builder.Services.AddMongoDBUserRepository<MongoDBContext, User>();
builder.Services.AddEFUserRepository<EFIdentityDbContext, User>();
// builder.Services.AddMongoDBUserTokenRepository<MongoDBContext>();
builder.Services.AddEFUserTokenRepository<EFIdentityDbContext>();
// builder.Services.AddMongoDBRoleRepository<MongoDBContext, Role>();
builder.Services.AddEFRoleRepository<EFIdentityDbContext, Role>();
// builder.Services.AddMongoDBUnitRepository<MongoDBContext, Unit>();
builder.Services.AddEFUnitRepository<EFIdentityDbContext, Unit>();
// builder.Services.AddMongoDBUnitTypeRepository<MongoDBContext, UnitType>();
builder.Services.AddEFUnitTypeRepository<EFIdentityDbContext, UnitType>();
// builder.Services.AddMongoDBUnitRoleRepository<MongoDBContext, UnitRole>();
builder.Services.AddEFUnitRoleRepository<EFIdentityDbContext, UnitRole>();

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
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCoreSwagger();

app.UseHttpLoggingMiddleware();

app.MapControllers();

app.Run();

// For access test projects
public partial class Program { }