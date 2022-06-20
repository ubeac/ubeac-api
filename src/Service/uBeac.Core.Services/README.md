## uBeac.Services: Provides entity service implementations
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Services?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Services/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Services?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Services/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Services
```

<hr>

### Create your entity services
Just create an class and inherit from ``EntityService``:
```cs
public class YourService : EntityService<YourEntityKey, YourEntity>
{
}

public class YourService : EntityService<YourEntity>
{
}
```
> Entity service works with repository.
