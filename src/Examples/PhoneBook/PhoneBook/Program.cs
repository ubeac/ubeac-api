using PhoneBook;
using uBeac.Repositories.MongoDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Adding json config files
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding swagger
builder.Services.AddCoreSwaggerWithJWT("Phone Book");

// Adding mongodb
builder.Services.AddMongo<MongoDBContext>("DefaultConnection");

// Adding repositories
builder.Services.AddScoped<IContactRepository, MongoContactRepository>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseCoreSwagger();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
