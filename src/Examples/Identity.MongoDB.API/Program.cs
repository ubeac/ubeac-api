using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using uBeac.Repositories.History.MongoDB;
using uBeac.Repositories.MongoDB;
using uBeac.Web;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files (IConfiguration)
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding http logging
builder.Services.AddMongoDbHttpLogging<HttpLogMongoDBContext>("HttpLoggingConnection", builder.Configuration.GetInstance<MongoDbHttpLogOptions>("HttpLogging"));

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

// Adding history
builder.Services.AddMongo<HistoryMongoDBContext>("HistoryConnection");
builder.Services.AddHistory<MongoDBHistoryRepository>().For<User>();

// Adding CORS
var corsPolicyOptions = builder.Configuration.GetSection("CorsPolicy");
builder.Services.AddCorsPolicy(corsPolicyOptions);

// Adding HSTS
var hstsOptions = builder.Configuration.GetSection("Hsts");
builder.Services.AddHttpsPolicy(hstsOptions);

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

app.UseHttpsRedirection();
app.UseHstsOnProduction(builder.Environment);
app.UseCorsPolicy(corsPolicyOptions);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCoreSwagger();

app.UseAuthentication();
app.UseHttpLoggingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();