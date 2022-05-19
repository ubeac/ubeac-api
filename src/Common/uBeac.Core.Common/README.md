## uBeac.Common: A common library for your .NET projects
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Common?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Common/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Common?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Common/)

[uBeac.Common](https://nuget.org/packages/uBeac.Common) includes base entities, models, interfaces. This package has no external dependencies and is the core of all uBeac packages.

### Why use?
In all / most .NET projects there are a series of common modules whose implementation only wastes your time. Instead of re-implementing each of these modules in different projects, you can use a common library. This library is maintained and developed by a good community and you do not need to be involved in infrastructure.

<hr>

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Common
```

<hr>

### Implement your entities
You can inherit from ``IEntity`` or ``Entity`` to implement your base entities:
```cs
public class YourEntity : IEntity<int>
{
}

public class YourEntity : IEntity
{
}

public class YourEntity : Entity<int>
{
}

public class YourEntity : Entity
{
}
```
> Default TKey of base entities is Guid. Therefore, if you do not specify TKey, Guid will be selected as TKey by default.

Or you can inherit from ``IAuditEntity`` or ``AuditEntity`` to implement your audit entities:
```cs
public class YourEntity : IAuditEntity<int>
{
}

public class YourEntity : IAuditEntity
{
}

public class YourEntity : AuditEntity<int>
{
}

public class YourEntity : AuditEntity
{
}
```
> ``IAuditEntity`` inherited from ``IEntity``, And in addition to Id, it has other properties like ``CreatedBy``, ``CreatedAt``, ``LastUpdatedBy``, ``LastUpdatedAt`` and etc.

<hr>

### Generate result
You can use two types of result models:
- ``IResult`` / ``Result``: This is base result model that contains ``TraceId``, ``SessionId``, ``Duration``, ``Code``, ``Errors`` and ``Debug``.
- ``IListResult`` / ``ListResult``: This is list result model that inherited from base result model, is used to return an IEnumerable<T> with pagination data ``PageSize``, ``TotalPages``, ``PageNumber``, ``TotalCount``, ``HasPrevious``, ``HasNext``.

There are several extension methods that you can use to generate results:
```cs
data.ToResult();
data.ToListResult();
```
  
<hr>
Enjoy!
