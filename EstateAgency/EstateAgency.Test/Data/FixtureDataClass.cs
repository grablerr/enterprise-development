using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;

namespace EstateAgency.Test.Data;
public class FixtureDataClass
{
    public List<EstateObject> EstateObjects =>
    [
        new EstateObject
        {
            Id = 1,
            Type = ObjectType.Apartment,
            Purpose = ObjectPurpose.Residential,
            CadastralNumber = "12:34:5678",
            Address = "ул. Ленина, д. 15, кв. 34",
            FloorNumber = 3,
            Floors = 5,
            Square = 65.5f,
            Rooms = 2,
            CeilingHeight = 270,
            IsEncumbrance = false

         },
        new EstateObject
        {
            Id = 2,
            Type = ObjectType.House,
            Purpose = ObjectPurpose.Residential,
            CadastralNumber = "23:45:6789",
            Address = "ул. Садовая, д. 42",
            FloorNumber = 1,
            Floors = 2,
            Square = 120.8f,
            Rooms = 4,
            CeilingHeight = 280,
            IsEncumbrance = true
        },
        new EstateObject
        {
            Id = 3,
            Type = ObjectType.Office,
            Purpose = ObjectPurpose.Commercial,
            CadastralNumber = "34:56:7890",
            Address = "пр. Мира, д. 88, оф. 305",
            FloorNumber = 3,
            Floors = 10,
            Square = 45.2f,
            Rooms = 1,
            CeilingHeight = 300,
            IsEncumbrance = false
        },
        new EstateObject
        {
            Id = 4,
            Type = ObjectType.Cottage,
            Purpose = ObjectPurpose.Residential,
            CadastralNumber = "45:67:8901",
            Address = "пос. Дачный, ул. Центральная, д. 7",
            FloorNumber = 1,
            Floors = 1,
            Square = 95.0f,
            Rooms = 3,
            CeilingHeight = 290,
            IsEncumbrance = false
        },
        new EstateObject
        {
            Id = 5,
            Type = ObjectType.Warehouse,
            Purpose = ObjectPurpose.Industrial,
            CadastralNumber = "56:78:9012",
            Address = "промзона, складской комплекс №5",
            FloorNumber = 1,
            Floors = 1,
            Square = 500.0f,
            Rooms = 0,
            CeilingHeight = 400,
            IsEncumbrance = true
        },
        new EstateObject
        {
            Id = 6,
            Type = ObjectType.Townhouse,
            Purpose = ObjectPurpose.Residential,
            CadastralNumber = "67:89:0123",
            Address = "ул. Парковая, д. 25",
            FloorNumber = 2,
            Floors = 3,
            Square = 145.0f,
            Rooms = 5,
            CeilingHeight = 275,
            IsEncumbrance = false
        },
        new EstateObject
        {
            Id = 7,
            Type = ObjectType.Shop,
            Purpose = ObjectPurpose.Commercial,
            CadastralNumber = "78:90:1234",
            Address = "ТЦ 'Европа', 1 этаж, пав. 12",
            FloorNumber = 1,
            Floors = 3,
            Square = 85.3f,
            Rooms = 1,
            CeilingHeight = 320,
            IsEncumbrance = false
        },
        new EstateObject
        {
            Id = 8,
            Type = ObjectType.Garage,
            Purpose = ObjectPurpose.Commercial,
            CadastralNumber = "89:01:2345",
            Address = "Гаражный кооператив 'Мотор', бокс 15",
            FloorNumber = 1,
            Floors = 1,
            Square = 25.0f,
            Rooms = 0,
            CeilingHeight = 250,
            IsEncumbrance = true
        },
        new EstateObject
        {
            Id = 9,
            Type = ObjectType.Apartment,
            Purpose = ObjectPurpose.Residential,
            CadastralNumber = "90:12:3456",
            Address = "пр. Победы, д. 112, кв. 89",
            FloorNumber = 9,
            Floors = 12,
            Square = 78.2f,
            Rooms = 3,
            CeilingHeight = 265,
            IsEncumbrance = false
        },
        new EstateObject
        {
            Id = 10,
            Type = ObjectType.Office,
            Purpose = ObjectPurpose.Commercial,
            CadastralNumber = "01:23:4567",
            Address = "Бизнес-центр 'Старт', оф. 405",
            FloorNumber = 4,
            Floors = 8,
            Square = 67.8f,
            Rooms = 2,
            CeilingHeight = 310,
            IsEncumbrance = true
        }
    ];

    public List<Counterparty> Counterparties =>
    [
        new Counterparty
        {
            Id = 1,
            FullName = "Ivan Ivanov",
            PassportNumber = "4500 123456",
            PhoneNumber = "+7 999 111-22-33"
        },
        new Counterparty
        {
            Id = 2,
            FullName = "Maria Petrova",
            PassportNumber = "4501 654321",
            PhoneNumber = "+7 999 222-33-44"
        },
        new Counterparty
        {
            Id = 3,
            FullName = "Sergey Sidorov",
            PassportNumber = "4502 789012",
            PhoneNumber = "+7 999 333-44-55"
        },
        new Counterparty
        {
            Id = 4,
            FullName = "Ekaterina Kozlova",
            PassportNumber = "4503 345678",
            PhoneNumber = "+7 999 444-55-66"
        },
        new Counterparty
        {
            Id = 5,
            FullName = "Dmitry Volkov",
            PassportNumber = "4504 901234",
            PhoneNumber = "+7 999 555-66-77"
        }
    ];

    public List<Application> Applications =>
    [
        new Application
        {
            Id = 1,
            AgentId = 1,
            AgentInfo = Counterparties[0],
            ObjectId = 1,
            ObjectInfo = EstateObjects[0],
            TransactionAmount = 8_000_000m,
            Type = ApplicationType.Sell,
            Date = new DateTime(2024, 5, 10)
        },
        new Application
        {
            Id = 2,
            AgentId = 2,
            AgentInfo = Counterparties[1],
            ObjectId = 2,
            ObjectInfo = EstateObjects[1], //house
            TransactionAmount = 12_500_000m,
            Type = ApplicationType.Buy,
            Date = new DateTime(2024, 5, 15)
        },
        new Application
        {
            Id = 3,
            AgentId = 3,
            AgentInfo = Counterparties[2],
            ObjectId = 3,
            ObjectInfo = EstateObjects[2],
            TransactionAmount = 5_200_000m,
            Type = ApplicationType.Sell,
            Date = new DateTime(2024, 5, 20)
        },
        new Application
        {
            Id = 4,
            AgentId = 4,
            AgentInfo = Counterparties[3],
            ObjectId = 4,
            ObjectInfo = EstateObjects[3],
            TransactionAmount = 9_800_000m,
            Type = ApplicationType.Buy,
            Date = new DateTime(2024, 5, 25)
        },
        new Application
        {
            Id = 5,
            AgentId = 5,
            AgentInfo = Counterparties[4],
            ObjectId = 5,
            ObjectInfo = EstateObjects[4],
            TransactionAmount = 18_000_000m,
            Type = ApplicationType.Sell,
            Date = new DateTime(2024, 6, 1)
        },
        new Application
        {
            Id = 6,
            AgentId = 1,
            AgentInfo = Counterparties[0],
            ObjectId = 6,
            ObjectInfo = EstateObjects[5],
            TransactionAmount = 15_300_000m,
            Type = ApplicationType.Buy,
            Date = new DateTime(2024, 6, 5)
        },
        new Application
        {
            Id = 7,
            AgentId = 2,
            AgentInfo = Counterparties[1],
            ObjectId = 7,
            ObjectInfo = EstateObjects[6],
            TransactionAmount = 6_700_000m,
            Type = ApplicationType.Sell,
            Date = new DateTime(2024, 6, 10)
        },
        new Application
        {
            Id = 8,
            AgentId = 3,
            AgentInfo = Counterparties[2],
            ObjectId = 8,
            ObjectInfo = EstateObjects[7],
            TransactionAmount = 3_500_000m,
            Type = ApplicationType.Buy,
            Date = new DateTime(2024, 6, 15)
        },
        new Application
        {
            Id = 9,
            AgentId = 1,
            AgentInfo = Counterparties[0],
            ObjectId = 9,
            ObjectInfo = EstateObjects[8],
            TransactionAmount = 10_100_000m,
            Type = ApplicationType.Sell,
            Date = new DateTime(2024, 6, 20)
        },
        new Application
        {
            Id = 10,
            AgentId = 2,
            AgentInfo = Counterparties[1],
            ObjectId = 10,
            ObjectInfo = EstateObjects[9],
            TransactionAmount = 7_900_000m,
            Type = ApplicationType.Buy,
            Date = new DateTime(2024, 6, 25)
        }
    ];

}
