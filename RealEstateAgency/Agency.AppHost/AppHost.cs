var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo").AddDatabase("db");

var batchSize = builder.AddParameter("GeneratorBatchSize");
var waitTime = builder.AddParameter("GeneratorWaitTime");

var grpcServer = builder.AddProject<Projects.Agency_Generator_Grpc_Host>("agency-generator-grpc-host")
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:WaitTime", waitTime);

builder.AddProject<Projects.Agency_Api_Host>("agency-api-host")
    .WithReference(db, "agencyClient")
    .WaitFor(db)
    .WithReference(grpcServer)
    .WithEnvironment("ContractRequestGenerator__GrpcAddress", grpcServer.GetEndpoint("https"))
    .WaitFor(grpcServer);

builder.Build().Run();
