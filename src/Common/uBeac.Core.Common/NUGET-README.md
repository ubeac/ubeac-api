## A Common Library For Your .NET Projects
uBeac.Common provides common interfaces, classes and extension methods in .NET projects without any external dependencies. Therefore, you can use single library / package instead of multiple libraries, packages and dependencies!
***
### Getting Started
Install the package with NuGet:
```
dotnet add package uBeac.Common
```
Or:
```
Install-Package uBeac.Common
```
***
### Common / Base / Audit Entities
Most projects require a common entity, which can be a class or an interface. You can use ``IEntity`` or ``Entity`` to implement your entities.
```
public class MyEntity : IEntity<int>
{
    public int Id { get; set; }
}

public class MyEntity : IEntity
{
    public Guid Id { get; set; }
}

public class MyEntity : Entity<int>
{
}

public class MyEntity : Entity
{
}
```
There is also another interface and class called ``IAuditEntity`` and ``AuditEntity`` that includes more properties like ``CreatedAt``, ``CreatedBy``, ``LastUpdatedAt`` and ``LastUpdatedBy``.
```
public class MyEntity : IAuditEntity<int>
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class MyEntity : IAuditEntity
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class MyEntity : AuditEntity<int>
{
}

public class MyEntity : AuditEntity
{
}
```
***
### Common Models
Each API consists of a set of request and result. In order to be able to provide good results, we need a base model of the result that is the same in our API. At uBeac.Common we offer two result models:
- ``IResult`` - ``Result`` : This is base result model that contains ``TraceId``, ``SessionId``, ``Duration``, ``Code``, ``Errors`` and ``Debug``
- ``IListResult`` - ``ListResult`` : Used to return a IEnumerable<T> with pagination data ``PageSize``, ``TotalPages``, ``PageNumber``, ``TotalCount``, ``HasPrevious``, ``HasNext`` - NOTE: This model inherits from ``IResult`` and ``Result``

There are also several extension methods that you can use to generate results:
- ``IListResult<T> ToListResult<T>(this IEnumerable<T> values)``
- ``IResult<T> ToResult<T>(this T value)``
- ``IListResult<T> ToListResult<T>(this Exception exception)``
- ``IResult<T> ToResult<T>(this Exception exception)``

For example:
```
 public async Task<IResult<bool>> Register(RegisterRequest request)
    {
        try
        {
            var data = await UserService.Register(request.UserName, request.Password);
            return data.ToResult();
        }
        catch (Exception ex)
        {
            return ex.ToResult<bool>();
        }
    }
```
***