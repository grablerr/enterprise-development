using Microsoft.AspNetCore.Mvc;
using EstateAgency.Application.AnalyticService;
using EstateAgency.Domain.Enums;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// API controller that exposes endpoints for analytic data retrieval.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Retrieves sellers grouped by period asynchronously.
    /// </summary>
    [HttpGet("sellers-by-period")]
    public async Task<IActionResult> GetSellersByPeriod()
    {
        var result = await analyticsService.GetSellersByPeriodAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves top clients by number of requests of specified application type.
    /// </summary>
    /// <param name="type">Type of application request</param>
    /// <param name="take">Number of top clients to return, default 5</param>
    [HttpGet("top-clients")]
    public async Task<IActionResult> GetTopClientsByRequests(
        [FromQuery] ApplicationType type,
        [FromQuery] int take = 5)
    {
        var result = await analyticsService.GetTopClients5ByRequestsAsync(type, take);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves count of requests grouped by real estate object type.
    /// </summary>
    [HttpGet("requests-by-object")]
    public async Task<IActionResult> GetRequestCountByObject()
    {
        var result = await analyticsService.GetRequestCountByObjectTypeAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves clients having requests with minimum price.
    /// </summary>
    [HttpGet("clients-with-min-price")]
    public async Task<IActionResult> GetClientsWithMinPriceRequests()
    {
        var result = await analyticsService.GetClientsWithMinPriceRequestsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves clients grouped by selected estate type.
    /// </summary>
    /// <param name="type">Type of real estate</param>
    [HttpGet("buyers-by-estate-type")]
    public async Task<IActionResult> GetClientsByEstateType([FromQuery] RealEstateType type)
    {
        var result = await analyticsService.GetClientsByEstateTypeAsync(type);
        return Ok(result);
    }
}