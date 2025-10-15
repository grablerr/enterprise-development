using EstateAgency.Application.Dtos;
using AutoMapper;
using EstateAgency.Domain.Enums;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Domain.Entities;

namespace EstateAgency.Application.AnalyticService;

/// <summary>
/// Provides analytic data service methods related to real estate applications,
/// counterparties, and real estate objects.
/// Utilizes repositories to fetch and process data, and AutoMapper to transform entities to DTOs.
/// </summary>
public class AnalyticsService(
    IRepository<EstateAgency.Domain.Entities.Application> applicationRepository,
    IRepository<Counterparty> counterpartyRepository,
    IRepository<RealEstate> realEstateRepository,
    IMapper mapper
)
{
    /// <summary>
    /// Retrieves sellers who have sold properties within a fixed period (May to June 2024).
    /// </summary>
    public async Task<List<CounterpartyDto>> GetSellersByPeriodAsync(DateTime from, DateTime to)
    {
        var applications = await applicationRepository.GetAllAsync();
        var counterparties = await counterpartyRepository.GetAllAsync();

        var sellers = applications
            .Where(a => a.Type == ApplicationType.Sell && a.Date >= from && a.Date <= to)
            .Join(counterparties,
                  a => a.CounterpartyId,
                  c => c.Id,
                  (a, c) => c)
            .Distinct()
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

        var result = applications
            .Where(a => a.Type == type)
            .Join(
                counterparties,
                app => app.CounterpartyId,
                cp => cp.Id,
                (app, cp) => cp
            )
            .GroupBy(c => c.Id)
            .Select(g => new CounterpartyDto
            {
                Id = g.Key,
                FullName = g.First().FullName,
                PassportNumber = g.First().PassportNumber,
                PhoneNumber = g.First().PhoneNumber,
                RequestCount = g.Count()
            })
            .OrderByDescending(c => c.RequestCount)
            .Take(take)
            .ToList();

        return result;
    }

    /// <summary>
    /// Retrieves a list of real estate objects with their request counts.
    /// </summary>
    public async Task<List<RealEstateTypeRequestCountDto>> GetRequestCountByObjectTypeAsync()
    {
        var applications = await applicationRepository.GetAllAsync();
        var estates = await realEstateRepository.GetAllAsync();

        var estateTypeById = estates.ToDictionary(e => e.Id, e => e.Type);

        var countsByType = applications
            .Where(a => estateTypeById.ContainsKey(a.RealEstateId))
            .GroupBy(a => estateTypeById[a.RealEstateId])
            .Select(g => new RealEstateTypeRequestCountDto
            {
                RealEstateType = g.Key.ToString(),
                Count = g.Count()
            })
            .OrderBy(r => r.RealEstateType)
            .ToList();

        return countsByType;
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