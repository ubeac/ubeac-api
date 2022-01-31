using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Middlewares;

args[1] = $"--contentRoot={Directory.GetCurrentDirectory()}";
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonConfig(builder.Environment);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddMongo<MongoDBContext>("DefaultConnection", true);

// Adding repositories
builder.Services.AddMongoDBUserRepository<MongoDBContext, User>();
builder.Services.AddMongoDBRoleRepository<MongoDBContext, Role>();
builder.Services.AddMongoDBUserTokenRepository<MongoDBContext>();
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
    })
    .AddIdentityRole<Role>(options =>
    {
        var adminRole = new Role("ADMIN");
        options.DefaultValues = new List<Role> { adminRole };
        options.AdminRole = adminRole;
    })
    .AddIdentityUnit<Unit>(options =>
    {
        var headquarter = new Unit { Name = "Management Office", Code = "1000", Type = "HQ" };
        var tehranBranch = new Unit { Name = "Tehran Central Branch", Code = "4000", Type = "BH", ParentUnitId = headquarter.Id.ToString() };
        var mirdamadBranch = new Unit { Name = "Mirdamad Branch", Code = "40001", Type = "BR", ParentUnitId = tehranBranch.Id.ToString() };
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

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
