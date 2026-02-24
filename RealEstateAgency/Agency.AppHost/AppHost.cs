var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo").AddDatabase("db");

builder.AddProject<Projects.Agency_Api_Host>("agency-api-host")
    .WithReference(db, "agencyClient")
    .WaitFor(db);

builder.Build().Run();
