## An Attractive Solution For Your JSON Configuration Files
It may have happened to you that your project involved many configurations and you had to put them all in one file called ``appsettings.json``. Well, everything is fine and there is no problem, but we at uBeac.Configuration offer you a more attractive solution.
## What Is The Solution?
Our suggestion is to create a file for each configuration and put it in a folder.
For example, we have a folder called ``Config`` that contains various json configuration files (like ``connection-strings.json``, ``authentication.json``, ``smtp.json`` or anything else.)

That is a lot better, is not it?
***
### Getting Started
Install the package with NuGet:
```
dotnet add package uBeac.Configuration
```
Or:
```
Install-Package uBeac.Configuration
```
***
### Configuration
Just put the following code in Program.cs:
```
builder.Configuration.AddJsonConfig(builder.Environment);
// builder.Configuration.AddJsonConfig(builder.Environment, "YourConfigFolderName");
// Default configuration folder name is "Config"
```
Now, you can add your json configuration files to the folder.
For example, if you use the default name, just create a new folder called "Config" and put your json configuration files in it.
***