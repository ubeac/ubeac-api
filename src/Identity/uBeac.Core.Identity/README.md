## uBeac.Identity: Provides identity management services
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Identity?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Identity?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Identity
```

<hr>

### Register
Just put the following code in Program.cs:
```cs
builder.Services.AddUserService<UserService<User>, User>();
builder.Services.AddRoleService<RoleService<Role>, Role>();
builder.Services.AddUserRoleService<UserRoleService<User>, User>();
builder.Services.AddUnitService<UnitService<Unit>, Unit>();
builder.Services.AddUnitTypeService<UnitTypeService<UnitType>, UnitType>();
builder.Services.AddUnitRoleService<UnitRoleService<UnitRole>, UnitRole>();
```
