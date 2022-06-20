## uBeac.Services.Abstractions: Provides base service interfaces
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Services.Abstractions?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Services.Abstractions/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Services.Abstractions?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Services.Abstractions/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Services.Abstractions
```

<hr>

### Create your service interfaces
Just create an interface and inherit from ``IService`` or ``IEntityService``:
```cs
public interface IYourService : IService
{
}

public interface IYourService : IEntityService<YourEntityKey, YourEntity>
{
}

public interface IYourService : IEntityService<YourEntity>
{
}
```
