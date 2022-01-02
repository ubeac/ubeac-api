using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Web.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddControllers();

builder.Configuration.AddJsonConfig(builder.Environment);

services.AddCoreSwaggerWithJWT("uBaec.Api", "v1");

services.AddMongo<MongoDBContext>("DefaultConnection");

//services.AddMappingProfile<MappingProfileForDTOs>();
//services.AddAuthenticationServices<Guid, User<Guid>, Guid, Role<Guid>>();
//services.AddJwtAuthentication(builder.Configuration.GetInstance<JwtOptions>("Jwt"));
//services.AddMongoDBIdentity<MongoDBContext, Guid, User<Guid>, Guid, Role<Guid>>();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseCoreSwagger();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
