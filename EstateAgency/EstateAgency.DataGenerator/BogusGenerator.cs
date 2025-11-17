using Bogus;
using EstateAgency.Contracts.Dtos;

namespace EstateAgency.DataGenerator;

/// <summary>
/// Data generator class that uses the Bogus library to create fake data transfer objects (DTOs)
/// for real estate, counterparties, and applications in the estate agency domain.
/// </summary>
public class BogusGenerator
{
    /// <summary>
    /// Faker instance to generate fake application creation DTOs.
    /// </summary>
    private readonly Faker<ApplicationCreateDto> _applicationsFaker;

    /// <summary>
    /// Faker instance to generate fake counterparty creation DTOs.
    /// </summary>
    private readonly Faker<CounterpartyCreateDto> _counterpartiesFaker;

    /// <summary>
    /// Faker instance to generate fake real estate creation DTOs.
    /// </summary>
    private readonly Faker<RealEstateCreateDto> _realEstatesFaker;

    /// <summary>
    /// Counter tracking the number of generated real estate records.
    /// </summary>
    private int _realEstatesCount;

    /// <summary>
    /// Counter tracking the number of generated counterparty records.
    /// </summary>
    private int _counterpartiesCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="BogusGenerator"/> class 
    /// with optional parameters for existing counts of estates and counterparties.
    /// Configures the Faker rules for generating realistic fake data for all DTO types.
    /// </summary>
    /// <param name="existingEstatesCount">Initial count of real estates to start from.</param>
    /// <param name="existingCounterpartiessCount">Initial count of counterparties to start from.</param>
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
            .RuleFor(x => x.CounterpartyId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.RealEstateId, f => f.Random.Int(1, 10))
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

    /// <summary>
    /// Generates a new fake real estate creation DTO and increments the real estate counter.
    /// </summary>
    /// <returns>A <see cref="RealEstateCreateDto"/> with randomized data.</returns>
    public RealEstateCreateDto GenerateRealEstate()
    {
        _realEstatesCount++;
        return _realEstatesFaker.Generate();
    }

    /// <summary>
    /// Generates a new fake counterparty creation DTO and increments the counterparty counter.
    /// </summary>
    /// <returns>A <see cref="CounterpartyCreateDto"/> with randomized data.</returns>
    public CounterpartyCreateDto GenerateCounterparty()
    {
        _counterpartiesCount++;
        return _counterpartiesFaker.Generate();
    }

    /// <summary>
    /// Generates a new fake application creation DTO.
    /// </summary>
    /// <returns>An <see cref="ApplicationCreateDto"/> with randomized data.</returns>
    public ApplicationCreateDto GenerateApplication() => _applicationsFaker.Generate();
}