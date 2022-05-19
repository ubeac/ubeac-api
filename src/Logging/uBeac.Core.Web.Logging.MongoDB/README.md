## uBeac.Web.Logging.MongoDB: Implement HTTP Logging in ASP.NET Core and MongoDB
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Web.Logging.MongoDB?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Logging.MongoDB/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Web.Logging.MongoDB?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Web.Logging.MongoDB/)

Using [uBeac.Web.Logging.MongoDB](https://nuget.org/packages/uBeac.Web.Logging.MongoDB) you can log and store HTTP request/response in MongoDB.

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Web.Logging.MongoDB
```

<hr>

### Add Middleware
Just put the following code in Program.cs:
```cs
app.UseHttpLoggingMiddleware();
```

<hr>

### Register Repository
Just put the following code in Program.cs:
```cs
builder.Services.AddMongoDbHttpLogging(builder.Configuration.GetInstance<HttpLoggingMongoDbOptions>("HttpLogging"));
```
And in appsettings.json:
```json
{
  "HttpLogging": {
    "HttpLog2xxConnectionString": "mongodb://localhost:27017/uBeac-Identity-Template-Http-Logging",
    "HttpLog2xxCollectionName": "HttpLog-2xx",
    "HttpLog4xxConnectionString": "mongodb://localhost:27017/uBeac-Identity-Template-Http-Logging",
    "HttpLog4xxCollectionName": "HttpLog-4xx",
    "HttpLog5xxConnectionString": "mongodb://localhost:27017/uBeac-Identity-Template-Http-Logging",
    "HttpLog5xxCollectionName": "HttpLog-5xx"
  }
}
```
- HTTP logs with response status code 100 to 399 are stored in 2xx database and collection.
- HTTP logs with response status code 400 to 499 are stored in 4xx database and collection.
- HTTP logs with response status code 500 to 599 are stored in 5xx database and collection.

The following scenarios can be implemented:
- Store all logs in a database and collection: In this scenario, all connection strings and collection names must be the same.
- Store all logs in a database and multiple collections: In this scenario, all connection strings are the same but the collection names are different.
- Store all logs in multiple databases: In this scenario, all connection strings are different.
