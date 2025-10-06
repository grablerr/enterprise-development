using EstateAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public DbSet<RealEstate> RealEstates { get; set; }

    public DbSet<Counterparty> Counterparties { get; set; }

    public DbSet<Application> Applications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Counterparty>(c =>
        {
            c.HasKey(c => c.Id);

            c.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            c.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            c.Property(c => c.PassportNumber)
                .IsRequired()
                .HasMaxLength(32);

            c.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
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
        });

        modelBuilder.Entity<Application>(a =>
        {
            a.HasKey(a => a.Id);
            a.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            a.Property(a => a.CounterpartyId)
                .IsRequired();

            a.HasOne(a => a.Counterparty)
                .WithMany()
                .HasForeignKey(e => e.CounterpartyId)
                .OnDelete(DeleteBehavior.Restrict);

            a.Property(a => a.RealEstateId)
                .IsRequired();

            a.HasOne(a => a.RealEstate)
                .WithMany()
                .HasForeignKey(e => e.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);

            a.Property(a => a.TransactionAmount)
                .IsRequired()
                .HasColumnType("decimal(12,2)");

            a.Property(a => a.Type)
                .HasConversion<string>()
                .IsRequired();

            a.Property(a => a.Date)
                .IsRequired()
                .HasColumnType("date");
        });
    }
}