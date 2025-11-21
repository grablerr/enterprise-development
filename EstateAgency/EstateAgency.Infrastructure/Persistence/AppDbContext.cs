using EstateAgency.Domain.Data;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EstateAgency.Infrastructure.Persistence;

/// <summary>
/// Application database context representing the relational database using Entity Framework Core.
/// Defines DbSet properties for RealEstate, Counterparty, and Application entities.
/// Configures entity mappings and relationships using Fluent API in OnModelCreating.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// RealEstates table representation.
    /// </summary>
    public DbSet<RealEstate> RealEstates { get; set; }

    /// <summary>
    /// Counterparties table representation.
    /// </summary>
    public DbSet<Counterparty> Counterparties { get; set; }

    /// <summary>
    /// Applications table representation.
    /// </summary>
    public DbSet<Application> Applications { get; set; }

    /// <summary>
    /// Configures entity mappings, primary keys, property constraints, and relationships.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var dataSeeder = new DataSeeder();

        modelBuilder.Entity<Counterparty>(c =>
        {
            c.HasKey(c => c.Id);

            c.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            c.Property(c => c.FullName)
                .IsRequired();

            c.Property(c => c.PassportNumber)
                .IsRequired()
                .HasMaxLength(11);

            c.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(16);
            c.HasData(dataSeeder.Counterparties);
        });

        modelBuilder.Entity<RealEstate>(r =>
        {
            r.HasKey(r => r.Id);

            r.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            r.Property(r => r.Type)
                .HasConversion<string>()
                .IsRequired();

            r.Property(r => r.Purpose)
                .HasConversion<string>()
                .IsRequired();

            r.Property(r => r.CadastralNumber)
                .IsRequired()
                .HasMaxLength(64);

            r.Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(256);

            r.Property(r => r.FloorNumber);

            r.Property(r => r.Floors);

            r.Property(r => r.Square)
                .IsRequired();

            r.Property(r => r.Rooms);

            r.Property(r => r.CeilingHeight);

            r.Property(r => r.IsEncumbrance)
                .IsRequired();
            r.HasData(dataSeeder.EstateObjects);
        });

        modelBuilder.Entity<Application>(a =>
        {
            a.HasKey(a => a.Id);
            a.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            a.HasOne<Counterparty>()
                .WithMany()
                .HasForeignKey(a => a.CounterpartyId)
                .OnDelete(DeleteBehavior.Cascade);

            a.HasOne<RealEstate>()
                .WithMany()
                .HasForeignKey(a => a.RealEstateId)
                .OnDelete(DeleteBehavior.Cascade);

            a.Property(a => a.TransactionAmount)
                .IsRequired()
                .HasColumnType("decimal(12,2)");

            a.Property(a => a.Type)
                .HasConversion<string>()
                .IsRequired();

            a.Property(a => a.Date)
                .IsRequired()
                .HasColumnType("date");
            a.HasData(dataSeeder.Applications);
        });
    }
}