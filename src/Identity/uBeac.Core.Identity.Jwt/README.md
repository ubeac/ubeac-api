## uBeac.Identity.Jwt: Provides JWT service for identity management
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Identity.Jwt?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.Jwt/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Identity.Jwt?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.Jwt/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Identity.Jwt
```

<hr>

### Register
Just put the following code in Program.cs:
```cs
builder.Services.AddJwtService<User>(builder.Configuration);
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
