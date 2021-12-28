using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Services;
using uBeac.Web.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonConfig(builder.Environment);
var services = builder.Services;
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));
//services.AddMappingProfile<MappingProfileForDTOs<Guid, User<Guid>, RegisterRequest, LoginRequest, LoginResponse<Guid>>>();
services.AddMappingProfile<MappingProfileForDTOs>();
services.AddJwtAuthentication(builder.Configuration.GetInstance<JwtOptions>("Jwt"));
services.AddAuthenticationServices<Guid, User<Guid>, Guid, Role<Guid>>();
services.AddCoreSwaggerWithJWT("uBaec.Api", "v1");
services.AddMongo<MongoDBContext>("DefaultConnection");
services.AddMongoDBIdentity<MongoDBContext, Guid, User<Guid>, Guid, Role<Guid>>();
services.AddHttpContextAccessor();
services.AddMvcCore()
    .AddDataAnnotations()
    .AddApiExplorer()
    .AddFormatterMappings();

if (!builder.Environment.IsDevelopment())
{
    services.AddCors();
}

services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCoreSwagger();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
