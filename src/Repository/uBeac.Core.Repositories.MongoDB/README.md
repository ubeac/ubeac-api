## uBeac.Repositories.MongoDB: Provides MongoDB repositories
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Repositories.MongoDB?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Repositories.MongoDB/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Repositories.MongoDB?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Repositories.MongoDB/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Repositories.MongoDB
```

<hr>

### Create your MongoDB repositories
Just create an class and inherit from ``MongoEntityRepository``:
```cs
public class YourRepository : MongoEntityRepository<YourEntityKey, YourEntity, YourMongoDbContext>
{
}

public class YourRepository : MongoEntityRepository<YourEntity, YourMongoDbContext>
{
}
```
>  You can use ``MongoDBContext`` or implement ``IMongoDBContext``.
