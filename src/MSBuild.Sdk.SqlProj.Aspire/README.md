# MSBuild.Sdk.SqlProj.Aspire
This package provides [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) integration for MSBuild.Sdk.SqlPproj projects. It allows you to publish SQL Database Projects as part of your .NET Aspire AppHost projects.

## Usage
To use this package, install it into your .NET Aspire AppHost project:

```bash
dotnet add package MSBuild.Sdk.SqlProj.Aspire
```

Next, add a reference to the MSBuild.Sdk.SqlProj project you want to publish in your .NET Aspire AppHost project:

```bash
dotnet add reference ../MySqlProj/MySqlProj.csproj
```

> Note: Adding this referene will currently result in warning ASPIRE004. This is a known issue and will be resolved in a future release.

Finally add the project as a resource to your .NET Aspire AppHost:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("test");

builder.AddDataTierApplication<Projects.MySqlProj>("mysqlproj")
       .PublishTo(sql);

builder.Build().Run();
```

Now when you run your .NET Aspire AppHost project you will see the SQL Database Project being published to the specified SQL Server.

## Local .dacpac file support
If you are sourcing your data-tier application package (.dacpac) file from somewhere other than a project reference, you can also specify the path to the .dacpac file directly:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("test");

builder.AddDataTierApplication("mysqlproj", "path/to/mysqlproj.dacpac")
       .PublishTo(sql);

builder.Build().Run();
```