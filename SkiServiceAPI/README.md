# SkiServiceAPI

ASP.NET Core Web API for SkiService running on net8.0.
Below are listed some instructions the help working on the project.

## Running the project

Ensure you got a mssql instance running on localhost. The connection string can be found in the appsettings.json file.
When running the project in docker the connection string will be overwritten by the environment.

## Creating a new migration

When the models where moved to the library project, the default migration method in the package manager console stopped working.

To create a new migration, run the following command in the Project (`SkiServiceAPI`) folder:

```powershell
dotnet ef migrations add <name>
```

if the command above is not found install `dotnet-ef` with the following command:

```powershell
dotnet tool install --global dotnet-ef
```
