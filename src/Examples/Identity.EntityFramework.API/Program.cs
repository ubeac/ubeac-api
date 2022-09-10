using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity.EntityFramework;
using uBeac.Web;
using uBeac.Web.Logging;
using uBeac.Web.Logging.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files (IConfiguration)
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding http logging
// builder.Services.AddSqlServerDatabase<HttpLogDbContext>(builder.Configuration.GetConnectionString("HttpLoggingConnection"));
// builder.Services.AddEFHttpLogging<HttpLogDbContext>();

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
builder.Services.AddSqlServerDatabase<IdentityCoreDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

// Adding history
// builder.Services.AddMongo<HistoryMongoDBContext>("HistoryConnection");
// builder.Services.AddHistory<MongoDBHistoryRepository>().For<User>();

// Adding CORS
var corsPolicyOptions = builder.Configuration.GetSection("CorsPolicy");
builder.Services.AddCorsPolicy(corsPolicyOptions);

// Adding HSTS
var hstsOptions = builder.Configuration.GetSection("Hsts");
builder.Services.AddHttpsPolicy(hstsOptions);

// Adding email provider
builder.Services.AddEmailProvider(builder.Configuration);

// Adding repositories
builder.Services.AddEFUserRepository<IdentityCoreDbContext, User>();
builder.Services.AddEFUserTokenRepository<IdentityCoreDbContext>();
builder.Services.AddEFRoleRepository<IdentityCoreDbContext, Role>();
builder.Services.AddEFUnitRepository<IdentityCoreDbContext, Unit>();
builder.Services.AddEFUnitTypeRepository<IdentityCoreDbContext, UnitType>();
builder.Services.AddEFUnitRoleRepository<IdentityCoreDbContext, UnitRole>();

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
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseHstsOnProduction(builder.Environment);
app.UseCorsPolicy(corsPolicyOptions);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCoreSwagger();

app.UseAuthentication();
// app.UseHttpLoggingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();