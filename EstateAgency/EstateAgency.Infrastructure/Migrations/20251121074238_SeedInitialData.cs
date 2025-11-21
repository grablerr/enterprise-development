using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Counterparties",
                columns: new[] { "Id", "FullName", "PassportNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Ivan Ivanov", "4500 123456", "+7 999 111-22-33" },
                    { 2, "Maria Petrova", "4501 654321", "+7 999 222-33-44" },
                    { 3, "Sergey Sidorov", "4502 789012", "+7 999 333-44-55" },
                    { 4, "Ekaterina Kozlova", "4503 345678", "+7 999 444-55-66" },
                    { 5, "Dmitry Volkov", "4504 901234", "+7 999 555-66-77" },
                    { 6, "Anna Smirnova", "4505 112233", "+7 999 666-77-88" },
                    { 7, "Pavel Novikov", "4506 445566", "+7 999 777-88-99" },
                    { 8, "Olga Kuznetsova", "4507 778899", "+7 999 888-99-00" },
                    { 9, "Mikhail Lebedev", "4508 556677", "+7 999 999-00-11" },
                    { 10, "Elena Morozova", "4509 334455", "+7 999 000-11-22" }
                });

            migrationBuilder.InsertData(
                table: "RealEstates",
                columns: new[] { "Id", "Address", "CadastralNumber", "CeilingHeight", "FloorNumber", "Floors", "IsEncumbrance", "Purpose", "Rooms", "Square", "Type" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, д. 15, кв. 34", "12:34:5678912:3456", 270.0, 3, 5, false, "Residential", 2, 65.5f, "Apartment" },
                    { 2, "ул. Садовая, д. 42", "23:45:6789012:7890", 280.0, 1, 2, true, "Residential", 4, 120.8f, "House" },
                    { 3, "пр. Мира, д. 88, оф. 305", "34:56:7890123:123", 300.0, 3, 10, false, "Commercial", 1, 45.2f, "Office" },
                    { 4, "пос. Дачный, ул. Центральная, д. 7", "45:67:8901234:56789", 290.0, 1, 1, false, "Residential", 3, 95f, "Cottage" },
                    { 5, "промзона, складской комплекс №5", "56:78:9012345:1", 400.0, 1, 1, true, "Industrial", 0, 500f, "Warehouse" },
                    { 6, "ул. Парковая, д. 25", "67:89:0123456:999999", 275.0, 2, 3, false, "Residential", 5, 145f, "Townhouse" },
                    { 7, "ТЦ 'Европа', 1 этаж, пав. 12", "78:90:1234567:88", 320.0, 1, 3, false, "Commercial", 1, 85.3f, "Shop" },
                    { 8, "Гаражный кооператив 'Мотор', бокс 15", "89:01:2345678:0", 250.0, 1, 1, true, "Commercial", 0, 25f, "Garage" },
                    { 9, "пр. Победы, д. 112, кв. 89", "90:12:3456789:123456789", 265.0, 9, 12, false, "Residential", 3, 78.2f, "Apartment" },
                    { 10, "Бизнес-центр 'Старт', оф. 405", "01:23:4567890:4567", 310.0, 4, 8, true, "Commercial", 2, 67.8f, "Office" }
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "CounterpartyId", "Date", "RealEstateId", "TransactionAmount", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8000000m, "Sell" },
                    { 2, 2, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12500000m, "Buy" },
                    { 3, 3, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5200000m, "Sell" },
                    { 4, 4, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 9800000m, "Buy" },
                    { 5, 5, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 18000000m, "Sell" },
                    { 6, 1, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 15300000m, "Buy" },
                    { 7, 2, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 6700000m, "Sell" },
                    { 8, 3, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3500000m, "Buy" },
                    { 9, 1, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 10100000m, "Sell" },
                    { 10, 2, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 7900000m, "Buy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Counterparties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
