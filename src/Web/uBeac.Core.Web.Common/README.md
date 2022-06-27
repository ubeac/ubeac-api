## uBeac.Web.Common: A common library for your ASP.NET Core projects
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Web.Common?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Common/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Web.Common?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Common/)

[uBeac.Web.Common](https://nuget.org/packages/uBeac.Web.Common) includes base controller, common action filters, application context and debugger implementation.

### Why use?
Probably the first thing you do after creating an ASP.NET Core project is to create a base controller and define action filters.
Well use [uBeac.Web.Common](https://nuget.org/packages/uBeac.Web.Common) instead.
Also, this package provides an implementation of application context and debugger which we will use in the following and you will understand their use.

<hr>

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Web.Common
```

<hr>

### Implement your controllers
You can inherit from ``BaseController`` to implement your controllers:
```cs
public class YourApiController : BaseController
{
}
```
> This will apply the routing pattern and action filters to the controller by default.

- Default routing pattern is ``API/[controller]/[action]``

<hr>

### Add application context
Just put the following code in Program.cs:
```cs
builder.Services.AddApplicationContext();
```
> With this, wherever you need application context properties (``TraceId``, ``UniqueId``, ``SessionId``, ``UserName``, ``UserIp``, ``Language``), just inject ``IApplicationContext``.

<hr>

### Add debugger
Just put the following code in Program.cs:
```cs
builder.Services.AddDebugger();
```
> With this, wherever you need to work with the debugger, just inject ``IDebugger``.
