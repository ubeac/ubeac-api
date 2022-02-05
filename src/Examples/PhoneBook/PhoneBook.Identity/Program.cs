var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Adding json config files
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding swagger
builder.Services.AddCoreSwaggerWithJWT("Phone Book - Identity");

// Adding mongodb
builder.Services.AddMongo<MongoDBContext>("DefaultConnection");

// Adding repositories
builder.Services.AddMongoDBUserRepository<MongoDBContext, AppUser>();
builder.Services.AddMongoDBUserTokenRepository<MongoDBContext>();
builder.Services.AddMongoDBRoleRepository<MongoDBContext, AppRole>();
builder.Services.AddMongoDBUnitRepository<MongoDBContext, AppUnit>();
builder.Services.AddMongoDBUnitTypeRepository<MongoDBContext, AppUnitType>();
builder.Services.AddMongoDBUnitRoleRepository<MongoDBContext, AppUnitRole>();

// Adding services
builder.Services.AddUserService<AppUserService, AppUser>();
builder.Services.AddRoleService<AppRoleService, AppRole>();
builder.Services.AddUserRoleService<AppUserRoleService, AppUser>();
builder.Services.AddUnitService<AppUnitService, AppUnit>();
builder.Services.AddUnitTypeService<AppUnitTypeService, AppUnitType>();
builder.Services.AddUnitRoleService<AppUnitRoleService, AppUnitRole>();

// Adding identity core
builder.Services
    .AddIdentityUser(configureOptions: Options.User)
    .AddIdentityRole(configureOptions: Options.Role)
    .AddIdentityUnit(configureOptions: Options.Unit)
    .AddIdentityUnitType(configureOptions: Options.UnitType);

// Adding jwt authentication
builder.Services.AddJwtAuthentication(builder.Configuration.GetInstance<JwtOptions>("Jwt"));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCoreSwagger();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
