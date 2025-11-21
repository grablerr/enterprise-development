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
            c.HasData(
            new Counterparty { Id = 1, FullName = "Ivan Ivanov", PassportNumber = "4500 123456", PhoneNumber = "+7 999 111-22-33" },
            new Counterparty { Id = 2, FullName = "Maria Petrova", PassportNumber = "4501 654321", PhoneNumber = "+7 999 222-33-44" },
            new Counterparty { Id = 3, FullName = "Sergey Sidorov", PassportNumber = "4502 789012", PhoneNumber = "+7 999 333-44-55" },
            new Counterparty { Id = 4, FullName = "Ekaterina Kozlova", PassportNumber = "4503 345678", PhoneNumber = "+7 999 444-55-66" },
            new Counterparty { Id = 5, FullName = "Dmitry Volkov", PassportNumber = "4504 901234", PhoneNumber = "+7 999 555-66-77" },
            new Counterparty { Id = 6, FullName = "Anna Smirnova", PassportNumber = "4505 112233", PhoneNumber = "+7 999 666-77-88" },
            new Counterparty { Id = 7, FullName = "Pavel Novikov", PassportNumber = "4506 445566", PhoneNumber = "+7 999 777-88-99" },
            new Counterparty { Id = 8, FullName = "Olga Kuznetsova", PassportNumber = "4507 778899", PhoneNumber = "+7 999 888-99-00" },
            new Counterparty { Id = 9, FullName = "Mikhail Lebedev", PassportNumber = "4508 556677", PhoneNumber = "+7 999 999-00-11" },
            new Counterparty { Id = 10, FullName = "Elena Morozova", PassportNumber = "4509 334455", PhoneNumber = "+7 999 000-11-22" }
        );
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
            r.HasData(
            new RealEstate { Id = 1, Type = RealEstateType.Apartment, Purpose = RealEstatePurpose.Residential, CadastralNumber = "12:34:5678912:3456", Address = "ул. Ленина, д. 15, кв. 34", FloorNumber = 3, Floors = 5, Square = 65.5f, Rooms = 2, CeilingHeight = 270, IsEncumbrance = false },
            new RealEstate { Id = 2, Type = RealEstateType.House, Purpose = RealEstatePurpose.Residential, CadastralNumber = "23:45:6789012:7890", Address = "ул. Садовая, д. 42", FloorNumber = 1, Floors = 2, Square = 120.8f, Rooms = 4, CeilingHeight = 280, IsEncumbrance = true },
            new RealEstate { Id = 3, Type = RealEstateType.Office, Purpose = RealEstatePurpose.Commercial, CadastralNumber = "34:56:7890123:123", Address = "пр. Мира, д. 88, оф. 305", FloorNumber = 3, Floors = 10, Square = 45.2f, Rooms = 1, CeilingHeight = 300, IsEncumbrance = false },
            new RealEstate { Id = 4, Type = RealEstateType.Cottage, Purpose = RealEstatePurpose.Residential, CadastralNumber = "45:67:8901234:56789", Address = "пос. Дачный, ул. Центральная, д. 7", FloorNumber = 1, Floors = 1, Square = 95.0f, Rooms = 3, CeilingHeight = 290, IsEncumbrance = false },
            new RealEstate { Id = 5, Type = RealEstateType.Warehouse, Purpose = RealEstatePurpose.Industrial, CadastralNumber = "56:78:9012345:1", Address = "промзона, складской комплекс №5", FloorNumber = 1, Floors = 1, Square = 500.0f, Rooms = 0, CeilingHeight = 400, IsEncumbrance = true },
            new RealEstate { Id = 6, Type = RealEstateType.Townhouse, Purpose = RealEstatePurpose.Residential, CadastralNumber = "67:89:0123456:999999", Address = "ул. Парковая, д. 25", FloorNumber = 2, Floors = 3, Square = 145.0f, Rooms = 5, CeilingHeight = 275, IsEncumbrance = false },
            new RealEstate { Id = 7, Type = RealEstateType.Shop, Purpose = RealEstatePurpose.Commercial, CadastralNumber = "78:90:1234567:88", Address = "ТЦ 'Европа', 1 этаж, пав. 12", FloorNumber = 1, Floors = 3, Square = 85.3f, Rooms = 1, CeilingHeight = 320, IsEncumbrance = false },
            new RealEstate { Id = 8, Type = RealEstateType.Garage, Purpose = RealEstatePurpose.Commercial, CadastralNumber = "89:01:2345678:0", Address = "Гаражный кооператив 'Мотор', бокс 15", FloorNumber = 1, Floors = 1, Square = 25.0f, Rooms = 0, CeilingHeight = 250, IsEncumbrance = true },
            new RealEstate { Id = 9, Type = RealEstateType.Apartment, Purpose = RealEstatePurpose.Residential, CadastralNumber = "90:12:3456789:123456789", Address = "пр. Победы, д. 112, кв. 89", FloorNumber = 9, Floors = 12, Square = 78.2f, Rooms = 3, CeilingHeight = 265, IsEncumbrance = false },
            new RealEstate { Id = 10, Type = RealEstateType.Office, Purpose = RealEstatePurpose.Commercial, CadastralNumber = "01:23:4567890:4567", Address = "Бизнес-центр 'Старт', оф. 405", FloorNumber = 4, Floors = 8, Square = 67.8f, Rooms = 2, CeilingHeight = 310, IsEncumbrance = true }
        );
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
            a.HasData(
            new Application { Id = 1, CounterpartyId = 1, RealEstateId = 1, TransactionAmount = 8_000_000m, Type = ApplicationType.Sell, Date = new DateTime(2024, 5, 10) },
            new Application { Id = 2, CounterpartyId = 2, RealEstateId = 2, TransactionAmount = 12_500_000m, Type = ApplicationType.Buy, Date = new DateTime(2024, 5, 15) },
            new Application { Id = 3, CounterpartyId = 3, RealEstateId = 3, TransactionAmount = 5_200_000m, Type = ApplicationType.Sell, Date = new DateTime(2024, 5, 20) },
            new Application { Id = 4, CounterpartyId = 4, RealEstateId = 4, TransactionAmount = 9_800_000m, Type = ApplicationType.Buy, Date = new DateTime(2024, 5, 25) },
            new Application { Id = 5, CounterpartyId = 5, RealEstateId = 5, TransactionAmount = 18_000_000m, Type = ApplicationType.Sell, Date = new DateTime(2024, 6, 1) },
            new Application { Id = 6, CounterpartyId = 1, RealEstateId = 6, TransactionAmount = 15_300_000m, Type = ApplicationType.Buy, Date = new DateTime(2024, 6, 5) },
            new Application { Id = 7, CounterpartyId = 2, RealEstateId = 7, TransactionAmount = 6_700_000m, Type = ApplicationType.Sell, Date = new DateTime(2024, 6, 10) },
            new Application { Id = 8, CounterpartyId = 3, RealEstateId = 8, TransactionAmount = 3_500_000m, Type = ApplicationType.Buy, Date = new DateTime(2024, 6, 15) },
            new Application { Id = 9, CounterpartyId = 1, RealEstateId = 9, TransactionAmount = 10_100_000m, Type = ApplicationType.Sell, Date = new DateTime(2024, 6, 20) },
            new Application { Id = 10, CounterpartyId = 2, RealEstateId = 10, TransactionAmount = 7_900_000m, Type = ApplicationType.Buy, Date = new DateTime(2024, 6, 25) }
        );
        });
    }
}