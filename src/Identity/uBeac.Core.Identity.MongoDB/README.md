## uBeac.Identity.MongoDB: Implements identity management repositories in MongoDB
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Identity.MongoDB?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.MongoDB/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Identity.MongoDB?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.MongoDB/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Identity.MongoDB
```

<hr>

### Register
Just put the following code in Program.cs:
```cs
builder.Services.AddMongoDBUserRepository<MongoDBContext, User>();
builder.Services.AddMongoDBUserTokenRepository<MongoDBContext>();
builder.Services.AddMongoDBRoleRepository<MongoDBContext, Role>();
builder.Services.AddMongoDBUnitRepository<MongoDBContext, Unit>();
builder.Services.AddMongoDBUnitTypeRepository<MongoDBContext, UnitType>();
builder.Services.AddMongoDBUnitRoleRepository<MongoDBContext, UnitRole>();
```
