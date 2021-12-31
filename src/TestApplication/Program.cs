using TestApplication;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;
using uBeac.Services;
using uBeac.Web.Identity;
using uBeac.Logging.MongoDB;
using Serilog;
using uBeac.Web.Logging;
using Serilog.Context;
using uBeac.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonConfig(builder.Environment);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.FromGlobalLogContext()
    .Enrich.WithAppEnricher()
    .Enrich.With<AppLogEnricher>()
    .AddApiLog().WriteToMongoDB(builder.Configuration.GetConnectionString("LogConnection")).CreateLogger();

GlobalLogContext.PushProperty("AppVersion", "GetThisAppVersion()");

var services = builder.Services;

services.AddScoped<ITestService, TestService>();

services.AddScoped<MethodLogs>();

services.Intercept<ITestService>().With<TestLogInterceptor>().Build();

services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));

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

app.UseMiddleware<ApiLogMiddleware>();
app.MapControllers();

app.Run();
