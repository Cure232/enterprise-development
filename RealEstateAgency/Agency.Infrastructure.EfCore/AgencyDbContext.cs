using Agency.Domain.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Agency.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных риэлторского агентства (MongoDB)
/// </summary>
public class AgencyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Counterparty> Counterparties => Set<Counterparty>();
    public DbSet<RealEstate> RealEstates => Set<RealEstate>();
    public DbSet<ContractRequest> ContractRequests => Set<ContractRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        modelBuilder.Entity<Counterparty>(builder =>
        {
            builder.ToCollection("counterparties");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasElementName("_id");
            builder.Property(c => c.FullName).HasElementName("full_name");
            builder.Property(c => c.PassportNumber).HasElementName("passport_number");
            builder.Property(c => c.PhoneNumber).HasElementName("phone_number");
        });

        modelBuilder.Entity<RealEstate>(builder =>
        {
            builder.ToCollection("real_estates");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasElementName("_id");
            builder.Property(r => r.Type).HasElementName("type");
            builder.Property(r => r.Purpose).HasElementName("purpose");
            builder.Property(r => r.CadastralNumber).HasElementName("cadastral_number");
            builder.Property(r => r.Address).HasElementName("address");
            builder.Property(r => r.TotalFloors).HasElementName("total_floors");
            builder.Property(r => r.TotalArea).HasElementName("total_area");
            builder.Property(r => r.NumberOfRooms).HasElementName("number_of_rooms");
            builder.Property(r => r.CeilingHeight).HasElementName("ceiling_height");
            builder.Property(r => r.Floor).HasElementName("floor");
            builder.Property(r => r.HasEncumbrances).HasElementName("has_encumbrances");
            builder.Property(r => r.EncumbrancesDescription).HasElementName("encumbrances_description");
        });

        modelBuilder.Entity<ContractRequest>(builder =>
        {
            builder.ToCollection("contract_requests");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasElementName("_id");
            builder.Property(r => r.CounterpartyId).HasElementName("counterparty_id");
            builder.Property(r => r.RealEstateId).HasElementName("real_estate_id");
            builder.Property(r => r.ContractRequestType).HasElementName("contract_request_type");
            builder.Property(r => r.Amount).HasElementName("amount");
            builder.Property(r => r.CreatedDate).HasElementName("created_date");
            builder.Property(r => r.Status).HasElementName("status");
        });
    }
}
