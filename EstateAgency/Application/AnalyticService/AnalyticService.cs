using Application.Dtos;
using AutoMapper;
using EstateAgency.Domain.Enums;
using EstateAgency.Domain.Interfaces;

namespace Application.AnalyticService;

/// <summary>
/// Provides analytic data service methods related to real estate applications,
/// counterparties, and real estate objects.
/// Utilizes repositories to fetch and process data, and AutoMapper to transform entities to DTOs.
/// </summary>
public class AnalyticsService(
    IApplicationRepository applicationRepository,
    ICounterpartyRepository counterpartyRepository,
    IRealEstateRepository realEstateRepository,
    IMapper mapper
)
{
    /// <summary>
    /// Retrieves sellers who have sold properties within a fixed period (May to June 2024).
    /// </summary>
    public async Task<List<CounterpartyDto>> GetSellersByPeriodAsync()
    {
        var from = new DateTime(2024, 5, 1);
        var to = new DateTime(2024, 6, 30);
        var applications = await applicationRepository.GetAllAsync();
        var counterparties = await counterpartyRepository.GetAllAsync();

        var sellers = applications
            .Where(a => a.Type == ApplicationType.Sell && a.Date >= from && a.Date <= to)
            .Join(counterparties,
                  a => a.CounterpartyId,
                  c => c.Id,
                  (a, c) => c)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<CounterpartyDto>>(sellers);
    }

    /// <summary>
    /// Retrieves top clients sorted by number of requests of a specified application type.
    /// </summary>
    /// <param name="type">Type of application request</param>
    /// <param name="take">Number of top clients to return, default is 5</param>
    public async Task<List<CounterpartyDto>> GetTopClients5ByRequestsAsync(ApplicationType type, int take = 5)
    {
        var applications = await applicationRepository.GetAllAsync();
        var counterparties = await counterpartyRepository.GetAllAsync();

        var names = applications
            .Where(a => a.Type == type)
            .Select(a => counterparties.FirstOrDefault(c => c.Id == a.CounterpartyId)?.FullName)
            .Where(name => name != null)!
            .Select(name => name!);

        var grouped = names
            .GroupBy(name => name)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Client)
            .Take(take)
            .ToList();

        var selectedCounterparties = counterparties
            .Where(c => grouped.Any(g => g.Client == c.FullName))
            .OrderByDescending(c => grouped.First(g => g.Client == c.FullName).Count)
            .ThenBy(c => c.FullName)
            .ToList();

        var result = mapper.Map<List<CounterpartyDto>>(selectedCounterparties);
        for (var i = 0; i < result.Count; i++)
            result[i].RequestCount = grouped[i].Count;

        return result;
    }

    /// <summary>
    /// Retrieves a list of real estate objects with their request counts.
    /// </summary>
    public async Task<List<RealEstateDto>> GetRequestCountByObjectTypeAsync()
    {
        var applications = await applicationRepository.GetAllAsync();
        var estates = await realEstateRepository.GetAllAsync();

        var countsByEstateId = applications
            .GroupBy(a => a.RealEstateId)
            .ToDictionary(g => g.Key, g => g.Count());

        var estatesWithCounts = estates
            .Where(e => countsByEstateId.ContainsKey(e.Id))
            .OrderBy(e => e.Type)
            .ThenBy(e => e.Address)
            .Select(e => new RealEstateDto
            {
                Type = e.Type,
                Purpose = e.Purpose,
                CadastralNumber = e.CadastralNumber,
                Address = e.Address,
                FloorNumber = e.FloorNumber,
                Floors = e.Floors,
                Square = e.Square,
                Rooms = e.Rooms,
                CeilingHeight = e.CeilingHeight,
                IsEncumbrance = e.IsEncumbrance,
                Count = countsByEstateId[e.Id]
            })
            .ToList();

        return estatesWithCounts;
    }

    /// <summary>
    /// Retrieves clients who made requests with the minimum transaction amount.
    /// </summary>
    public async Task<List<CounterpartyDto>> GetClientsWithMinPriceRequestsAsync()
    {
        var applications = await applicationRepository.GetAllAsync();
        var counterparties = await counterpartyRepository.GetAllAsync();

        var minPrice = applications.Min(a => a.TransactionAmount);

        var clients = applications
            .Where(a => a.TransactionAmount == minPrice)
            .Join(counterparties,
                  a => a.CounterpartyId,
                  c => c.Id,
                  (a, c) => c)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<CounterpartyDto>>(clients);
    }

    /// <summary>
    /// Retrieves clients who bought real estate of a given type.
    /// </summary>
    /// <param name="targetType">Type of real estate</param>
    public async Task<List<CounterpartyDto>> GetClientsByEstateTypeAsync(RealEstateType targetType)
    {
        var applications = await applicationRepository.GetAllAsync();
        var estates = await realEstateRepository.GetAllAsync();
        var counterparties = await counterpartyRepository.GetAllAsync();

        var buyers = applications
            .Where(a => a.Type == ApplicationType.Buy)
            .Join(estates,
                 a => a.RealEstateId,
                 e => e.Id,
                 (a, e) => new { a.CounterpartyId, e.Type })
            .Where(x => x.Type == targetType)
            .Join(counterparties,
                 x => x.CounterpartyId,
                 c => c.Id,
                 (x, c) => c)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<CounterpartyDto>>(buyers);
    }
}