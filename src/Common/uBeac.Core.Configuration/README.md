## uBeac.Configuration: An attractive solution for your JSON configuration files!
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Configuration?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Configuration/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Configuration?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Configuration/)

It may have happened to you that your project involved many configurations and you had to put them all in one file called ``appsettings.json``. Well, everything is fine and there is no problem, but we at [uBeac.Configuration](https://nuget.org/packages/uBeac.Configuration) offer you a more attractive solution.

### What is the solution?
Our suggestion is to create a file for each configuration and put it in a folder. For example, we have a folder called ``Config`` that contains various JSON configuration files (like ``connection-strings.json``, ``authentication.json``, ``smtp.json`` or anything else.)

<hr>

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Configuration
```

<hr>


### Register
Just put the following code in Program.cs:
```cs
builder.Configuration.AddJsonConfig(builder.Environment, "YourConfigFolderName");
```
> ``"YourConfigFolderName"`` is the name of the folder in which your JSON files are located. Default is ``"Config"``.

For example:
- Config
  - connection-strings.json
  - authentication.json
  - smtp.json
> In this example, folder name is ``Config`` which contains JSON files.

> The ``Config`` folder must be at the root of the project.
