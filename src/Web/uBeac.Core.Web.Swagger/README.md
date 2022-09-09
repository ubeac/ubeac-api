## uBeac.Web.Swagger: Easily register Swagger in ASP.NET Core applications
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Web.Swagger?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Swagger/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Web.Swagger?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Swagger/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Web.Swagger
```

<hr>

Just put the following code in Program.cs:
```cs
builder.Services.AddCoreSwaggerWithJWT();

app.UseCoreSwagger();
```
