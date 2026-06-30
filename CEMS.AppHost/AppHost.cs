var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CEMS>("cems");

builder.Build().Run();
