var builder = DistributedApplication.CreateBuilder(args);

var databaseProject = builder.AddProject<Projects.TestProject>("db");

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("test")
                 .WithDatabaseProject(databaseProject);

builder.Build().Run();
