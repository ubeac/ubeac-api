using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Middlewares;

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
services.AddMongoDBRoleRepository<MongoDBContext, Role>();

// Adding services
services.AddUserService<UserService<User>, User>();
services.AddRoleService<RoleService<Role>, Role>();

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

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
