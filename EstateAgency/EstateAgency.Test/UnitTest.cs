using EstateAgency.Domain.Enums;
using EstateAgency.Test.Data;

namespace EstateAgency.Test;

/// <summary>
/// Contains unit tests for verifying business intelligence queries on real estate agency data.
/// Tests cover sales analytics, client ranking, property statistics, and financial analysis.
/// Uses FixtureDataClass as a shared test data fixture for consistent data across all tests.
/// </summary>
public class QueriesTests(FixtureDataClass testData) : IClassFixture<FixtureDataClass>
{

    /// <summary>
    /// Tests retrieval of all sellers who submitted sale applications within a specified date range.
    /// </summary>
    [Fact]
    public void GetSellersByPeriod()
    {
        var from = new DateTime(2024, 5, 1);
        var to = new DateTime(2024, 6, 30);
        var expected = new[] { "Ivan Ivanov", "Sergey Sidorov", "Dmitry Volkov", "Maria Petrova" };

        // Получаем имена контрагентов, применяя фильтр по типу и дате
        var sellers = testData.Applications
            .Where(r => r.Type == ApplicationType.Sell && r.Date >= from && r.Date <= to)
            .Select(r => testData.Counterparties.FirstOrDefault(c => c.Id == r.CounterpartyId)?.FullName)
            .Where(name => name != null)
            .Distinct()
            .ToList();

        Assert.Equal(expected, sellers);
    }


    /// <summary>
    /// Tests identification of top 5 clients by number of applications submitted, separated by transaction type.
    /// </summary>
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

        // Получаем топ покупателей
        var topBuyers = testData.Applications
            .Where(r => r.Type == ApplicationType.Buy)
            .Select(r => testData.Counterparties.FirstOrDefault(c => c.Id == r.CounterpartyId)?.FullName)
            .Where(name => name != null)
            .GroupBy(name => name)
            .Select(g => new { Client = g.Key!, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Client)
            .Take(5)
            .Select(x => x.Client)
            .ToList();

        // Получаем топ продавцов
        var topSellers = testData.Applications
            .Where(r => r.Type == ApplicationType.Sell)
            .Select(r => testData.Counterparties.FirstOrDefault(c => c.Id == r.CounterpartyId)?.FullName)
            .Where(name => name != null)
            .GroupBy(name => name)
            .Select(g => new { Client = g.Key!, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Client)
            .Take(5)
            .Select(x => x.Client)
            .ToList();

        Assert.Equal(expectedTopBuyers, topBuyers);
        Assert.Equal(expectedTopSellers, topSellers);
    }


    /// <summary>
    /// Tests calculation of application count distribution across different real estate property types.
    /// </summary>
    [Fact]
    public void GetRequestCountByObjectType()
    {
        var expectedCount = new Dictionary<ObjectType, int>
    {
        { ObjectType.Apartment, 2 },
        { ObjectType.House, 1 },
        { ObjectType.Office, 2 },
        { ObjectType.Cottage, 1 },
        { ObjectType.Warehouse, 1 },
        { ObjectType.Townhouse, 1 },
        { ObjectType.Shop, 1 },
        { ObjectType.Garage, 1 }
    };

        // Сгруппировать заявки по типу объекта недвижимости,
        // полученному через testData.RealEstates по RealEstateId
        var stats = testData.Applications
            .Select(r => testData.EstateObjects.FirstOrDefault(re => re.Id == r.RealEstateId)?.Type)
            .Where(type => type != null)
            .GroupBy(type => type.Value)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToDictionary(x => x.Type, x => x.Count);

        foreach (var (objectType, count) in stats)
            Assert.Equal(expectedCount[objectType], count);
    }


    /// <summary>
    /// Tests identification of clients associated with applications having the minimum transaction amount.
    /// </summary>
    [Fact]
    public void GetClientsWithMinPriceRequests()
    {
        const decimal expectedMinPrice = 3_500_000m;
        var expectedClient = new[] { "Sergey Sidorov" };

        // Вычисляем минимальную сумму по всем заявкам
        var minPrice = testData.Applications.Min(r => r.TransactionAmount);

        // Получаем имена контрагентов, у заявок с минимальной суммой
        var clients = testData.Applications
            .Where(r => r.TransactionAmount == minPrice)
            .Select(r => testData.Counterparties.FirstOrDefault(c => c.Id == r.CounterpartyId)?.FullName)
            .Where(name => name != null)
            .Distinct()
            .ToList();

        Assert.Equal(expectedMinPrice, minPrice);
        Assert.Equal(expectedClient, clients);
    }


    /// <summary>
    /// Tests retrieval of clients interested in purchasing specific types of real estate properties.
    /// </summary>
    [Fact]
    public void GetClientsByEstateType()
    {
        const ObjectType targetType = ObjectType.House;
        var expectedClients = new[] { "Maria Petrova" };

        var clients = testData.Applications
            .Where(r => r.Type == ApplicationType.Buy)
            .Where(r => testData.EstateObjects.FirstOrDefault(re => re.Id == r.RealEstateId)?.Type == targetType)
            .Select(r => testData.Counterparties.FirstOrDefault(c => c.Id == r.CounterpartyId)?.FullName)
            .Where(name => name != null)
            .Distinct()
            .Order()
            .ToList();

        Assert.Equal(expectedClients, clients);
    }

}