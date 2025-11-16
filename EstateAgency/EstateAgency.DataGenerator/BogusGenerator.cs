using Bogus;
using EstateAgency.Contracts.Dtos;

namespace EstateAgency.DataGenerator;

public class BogusGenerator
{
    private readonly Faker<ApplicationCreateDto> _applicationsFaker;

    private readonly Faker<CounterpartyCreateDto> _counterpartiesFaker;

    private readonly Faker<RealEstateCreateDto> _realEstatesFaker;

    private int _realEstatesCount;

    private int _counterpartiesCount;
    public BogusGenerator(int existingEstatesCount = 0, int existingCounterpartiessCount = 0)
    {
        _realEstatesCount = existingEstatesCount;
        _counterpartiesCount = existingCounterpartiessCount;

        var purposes = new[]
        {
            "Residential",
            "Commercial",
            "Industrial",
            "Agricultural"
        };

        var estateTypes = new[] {
            "Apartment",
            "House",
            "Cottage",
            "Office",
            "Townhouse",
            "Shop",
            "Warehouse",
            "Garage"
        };

        _applicationsFaker = new Faker<ApplicationCreateDto>()
            .RuleFor(x => x.CounterpartyId, f => f.Random.Int(1, 100))
            .RuleFor(x => x.RealEstateId, f => f.Random.Int(1, 1000))
            .RuleFor(x => x.TransactionAmount, f => Math.Round(f.Finance.Amount(10000, 10000000), 2))
            .RuleFor(x => x.Type, f => f.PickRandom(new[] { "Buy", "Sell" }))
            .RuleFor(x => x.Date, f => f.Date.Past(2));

        _counterpartiesFaker = new Faker<CounterpartyCreateDto>()
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.PassportNumber, f => f.Random.Replace("###########").Substring(0, 11))
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+7##########"));

        _realEstatesFaker = new Faker<RealEstateCreateDto>()
            .RuleFor(x => x.Type, f => f.PickRandom(estateTypes))
            .RuleFor(x => x.Purpose, f => f.PickRandom(purposes))
            .RuleFor(x => x.CadastralNumber, f => f.Random.Replace("##-##-##-#########"))
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.FloorNumber, f => f.Random.Bool(0.7f) ? f.Random.Int(1, 25) : (int?)null)
            .RuleFor(x => x.Floors, f => f.Random.Bool(0.7f) ? f.Random.Int(1, 30) : (int?)null)
            .RuleFor(x => x.Square, f => MathF.Round(f.Random.Float(20.0f, 500.0f), 2))
            .RuleFor(x => x.Rooms, f => f.Random.Bool(0.8f) ? f.Random.Int(1, 10) : (int?)null)
            .RuleFor(x => x.CeilingHeight, f => f.Random.Bool(0.6f) ? Math.Round(f.Random.Double(2.0, 5.0), 2) : (double?)null)
            .RuleFor(x => x.IsEncumbrance, f => f.Random.Bool(0.1f));
    }

    public RealEstateCreateDto GenerateBook()
    {
        _realEstatesCount++;
        return _realEstatesFaker.Generate();
    }

    public CounterpartyCreateDto GenerateCustomer()
    {
        _counterpartiesCount++;
        return _counterpartiesFaker.Generate();
    }

    public ApplicationCreateDto GenerateRecord() => _applicationsFaker.Generate();
}