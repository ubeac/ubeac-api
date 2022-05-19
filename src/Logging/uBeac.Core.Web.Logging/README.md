## uBeac.Web.Logging: Implement HTTP Logging in ASP.NET Core
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Web.Logging?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Logging/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Web.Logging?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Logging/)

Using [uBeac.Web.Logging](https://nuget.org/packages/uBeac.Web.Logging) you can log and store HTTP request/response.

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Web.Logging
```

<hr>

### Add Middleware
Just put the following code in Program.cs:
```cs
app.UseHttpLoggingMiddleware();
```

<hr>

### Implement Repository / Store
You must implement ``IHttpLoggingRepository`` to store logs:
```cs
public class HttpLoggingRepository : IHttpLoggingRepository
{
}
```
And register it in dependency container:
```cs
builder.Services.AddScoped<IHttpLoggingRepository, HttpLoggingRepository>();
```
Or you can use the following packages:
- [uBeac.Web.Logging.MongoDB](./uBeac.Core.Web.Logging.MongoDB): For storing in MongoDB
