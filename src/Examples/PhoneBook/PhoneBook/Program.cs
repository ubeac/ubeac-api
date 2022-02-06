using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

// Adding services
builder.Services.AddScoped<IContactService, ContactService>();

// Adding jwt authentication
builder.Services.AddAuthentication(configureOptions =>
    {
        configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        var secret = builder.Configuration.GetValue<string>("Jwt:Secret");
        var audience = builder.Configuration.GetValue<string>("Jwt:Audience");
        var issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");

        var key = Encoding.ASCII.GetBytes(secret);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            SaveSigninToken = true,
            ValidAudience = audience,
            ValidIssuer = issuer
        };
    });

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
