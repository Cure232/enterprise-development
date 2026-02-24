using Agency.Api.Host.Grpc;
using Agency.Application;
using Agency.Application.Contracts;
using Agency.Application.Contracts.ContractRequests;
using Agency.Application.Contracts.Counterparties;
using Agency.Application.Contracts.Protos;
using Agency.Application.Contracts.RealEstates;
using Agency.Application.Services;
using Agency.Domain;
using Agency.Domain.Data;
using Agency.Domain.Model;
using Agency.Infrastructure.EfCore;
using Agency.Infrastructure.EfCore.Repositories;
using Agency.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AgencyProfile());
    config.AddProfile(new AgencyGrpcProfile());
});

builder.Services.AddTransient<IRepository<Counterparty, int>, CounterpartyRepository>();
builder.Services.AddTransient<IRepository<RealEstate, int>, RealEstateRepository>();
builder.Services.AddTransient<IRepository<ContractRequest, int>, ContractRequestRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ICounterpartyService, CounterpartyService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IContractRequestService, ContractRequestService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name?.StartsWith("Agency") == true))
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddMongoDBClient("agencyClient");
builder.Services.AddDbContext<AgencyDbContext>((services, o) =>
{
    var db = services.GetRequiredService<IMongoDatabase>();
    o.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

builder.Services.AddGrpcClient<ContractRequestGeneratorGrpcService.ContractRequestGeneratorGrpcServiceClient>(o =>
{
    var addr = builder.Configuration["ContractRequestGenerator:GrpcAddress"]
               ?? throw new InvalidOperationException("ContractRequestGenerator:GrpcAddress is not configured");
    o.Address = new Uri(addr);
});

builder.Services.AddMemoryCache();

builder.Services.AddHostedService<AgencyGrpcClient>();

var app = builder.Build();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AgencyDbContext>();
    var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeeder>();

    if (!await dbContext.Counterparties.AnyAsync())
    {
        foreach (var c in dataSeed.Counterparties)
            await dbContext.Counterparties.AddAsync(c);
        foreach (var r in dataSeed.RealEstates)
            await dbContext.RealEstates.AddAsync(r);
        foreach (var req in dataSeed.ContractRequests)
            await dbContext.ContractRequests.AddAsync(req);
        await dbContext.SaveChangesAsync();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
