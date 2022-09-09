## uBeac.Web.Jwt: Easily register JWT in ASP.NET Core applications
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Web.Jwt?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Jwt/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Web.Jwt?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Jwt/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Web.Jwt
```

<hr>

### Register
Just put the following code in Program.cs:
```cs
builder.Services.AddJwtAuthentication(builder.Configuration);

app.UseAuthentication();
```
And in appsettings.json:
```json
{
  "Jwt": {
    "Issuer": "https://localhost:44352",
    "Audience": "https://localhost:44352",
    "TokenExpiry": 3600, // seconds
    "RefreshTokenExpiry": 36000, // seconds
    "Secret": "THIS SHOULD BE A COMPLEX SECRET!"
  }
}
```
