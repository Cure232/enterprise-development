using Agency.Generator.Grpc.Host.Grpc;
using Agency.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AgencyGeneratorGrpcProfile());
});

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<AgencyGrpcGeneratorService>();

app.Run();
