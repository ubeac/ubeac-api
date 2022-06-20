## uBeac.Repositories.Abstractions: Provides base repository interfaces
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Repositories.Abstractions?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Repositories.Abstractions/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Repositories.Abstractions?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Repositories.Abstractions/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Repositories.Abstractions
```

<hr>

### Create your repository interfaces
Just create an interface and inherit from ``IRepository`` or ``IEntityRepository``:
```cs
public interface IYourRepository : IRepository
{
}

public interface IYourRepository : IEntityRepository<YourEntityKey, YourEntity>
{
}

public interface IYourRepository : IEntityRepository<YourEntity>
{
}
```
