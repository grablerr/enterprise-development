using EstateAgency.Domain.Enums;
using EstateAgency.Test.Data;

namespace EstateAgency.Test;

public class QueriesTests(FixtureDataClass testData) : IClassFixture<FixtureDataClass>
{
    private readonly FixtureDataClass _testData = testData;


    [Fact]
    public void GetSellersByPeriod()
    {
        var from = new DateTime(2024, 5, 1);
        var to = new DateTime(2024, 6, 30);
        var expected = new[] { "Ivan Ivanov", "Sergey Sidorov", "Dmitry Volkov", "Maria Petrova", };

        var sellers = _testData.Applications
            .Where(r => r.Type == ApplicationType.Sell && r.Date >= from && r.Date <= to)
            .Select(r => r.AgentInfo!.FullName)
            .Distinct()
            .ToList();

        Assert.Equal(expected, sellers);
    }

    [Fact]
    public void GetTop5ClientsByRequests()
    {
        var expectedTopBuyers = new[]
        {
            "Maria Petrova",
            "Ekaterina Kozlova",
            "Ivan Ivanov",
            "Sergey Sidorov"

        };

        var expectedTopSellers = new[]
        {
            "Ivan Ivanov",
            "Dmitry Volkov",
            "Maria Petrova",
            "Sergey Sidorov"
        };

        var topBuyers = _testData.Applications
            .Where(r => r.Type == ApplicationType.Buy)
            .GroupBy(r => r.AgentInfo!.FullName)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Client)
            .Take(5)
            .Select(x => x.Client)
            .ToList();

        var topSellers = _testData.Applications
            .Where(r => r.Type == ApplicationType.Sell)
            .GroupBy(r => r.AgentInfo!.FullName)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Client)
            .Take(5)
            .Select(x => x.Client)
            .ToList();

        Assert.Equal(expectedTopBuyers, topBuyers);
        Assert.Equal(expectedTopSellers, topSellers);
    }


    [Fact]
    public void GetRequestCountByObjectType()
    {
        const int expectedApartments = 2;
        const int expectedHouses = 1;
        const int expectedOffices = 2;
        const int expectedCottages = 1;
        const int expectedWarehouses = 1;
        const int expectedTownhouses = 1;
        const int expectedShops = 1;
        const int expectedGarages = 1;

        var stats = _testData.Applications
            .GroupBy(r => r.ObjectInfo!.Type)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToDictionary(x => x.Type, x => x.Count);

        Assert.Equal(expectedApartments, stats[ObjectType.Apartment]);
        Assert.Equal(expectedHouses, stats[ObjectType.House]);
        Assert.Equal(expectedOffices, stats[ObjectType.Office]);
        Assert.Equal(expectedCottages, stats[ObjectType.Cottage]);
        Assert.Equal(expectedWarehouses, stats[ObjectType.Warehouse]);
        Assert.Equal(expectedTownhouses, stats[ObjectType.Townhouse]);
        Assert.Equal(expectedShops, stats[ObjectType.Shop]);
        Assert.Equal(expectedGarages, stats[ObjectType.Garage]);
    }

    [Fact]
    public void GetClientsWithMinPriceRequests()
    {
        const decimal expectedMinPrice = 3_500_000m;
        var expectedClient = new[] { "Sergey Sidorov" };

        var minPrice = _testData.Applications.Min(r => r.TransactionAmount);

        var clients = _testData.Applications
            .Where(r => r.TransactionAmount == minPrice)
            .Select(r => r.AgentInfo!.FullName)
            .Distinct()
            .ToList();

        Assert.Equal(expectedMinPrice, minPrice);
        Assert.Equal(expectedClient, clients);
    }

    [Fact]
    public void GetClientsByEstateType()
    {
        const ObjectType targetType = ObjectType.House;
        var expectedClients = new[] { "Maria Petrova" };

        var clients = _testData.Applications
            .Where(r => r.Type == ApplicationType.Buy && r.ObjectInfo!.Type == targetType)
            .Select(r => r.AgentInfo!.FullName)
            .Distinct()
            .Order()
            .ToList();

        Assert.Equal(expectedClients, clients);
    }
}