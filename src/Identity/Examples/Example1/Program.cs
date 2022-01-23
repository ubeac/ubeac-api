using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Middlewares;
using Helper = Example1.Helper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddControllers();

builder.Configuration.AddJsonConfig(builder.Environment);

services.AddCoreSwaggerWithJWT("Authentication - Example 1");

services.AddMongo<MongoDBContext>("DefaultConnection");

#region Identity

// Adding repositories
services.AddMongoDBUserRepository<MongoDBContext, User>();
services.AddMongoDBUserTokenRepository<MongoDBContext>();
services.AddMongoDBRoleRepository<MongoDBContext, Role>();
services.AddMongoDBUnitRepository<MongoDBContext, Unit>();
services.AddMongoDBUnitTypeRepository<MongoDBContext, UnitType>();
services.AddMongoDBUnitRoleRepository<MongoDBContext, UnitRole>();

// Adding services
services.AddUserService<UserService<User>, User>();
services.AddRoleService<RoleService<Role>, Role>();
services.AddUserRoleService<UserRoleService<User>, User>();
services.AddUnitService<UnitService<Unit>, Unit>(Helper.DefaultUnitOptions());
services.AddUnitTypeService<UnitTypeService<UnitType>, UnitType>(Helper.DefaultUnitTypeOptions());
services.AddUnitRoleService<UnitRoleService<UnitRole>, UnitRole>();

// Adding Core Identity
services
    .AddIdentityUser<User>()
    .AddIdentityRole<Role>();

services.AddJwtAuthentication(builder.Configuration.GetInstance<JwtOptions>("Jwt"));

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
