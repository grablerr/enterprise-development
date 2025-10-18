using Microsoft.AspNetCore.Mvc;
using EstateAgency.Application.AnalyticService;
using EstateAgency.Domain.Enums;
using EstateAgency.Application.Dtos;

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
    public async Task<ActionResult<List<CounterpartyDto>>> GetSellersByPeriod([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var result = await analyticsService.GetSellersByPeriodAsync(from, to);
        if (result == null || result.Count == 0)
            return NotFound();
        return result;
    }

    /// <summary>
    /// Retrieves top clients by number of requests of specified application type.
    /// </summary>
    /// <param name="type">Type of application request</param>
    /// <param name="take">Number of top clients to return, default 5</param>
    [HttpGet("top-clients")]
    public async Task<ActionResult<List<RequestCountDto>>> GetTopClientsByRequests(
        [FromQuery] string type,
        [FromQuery] int take = 5)
    {
        if (!Enum.TryParse<ApplicationType>(type, true, out var appType))
        {
            return BadRequest($"Invalid ApplicationType: {type}");
        }

        var result = await analyticsService.GetTopClients5ByRequestsAsync(appType, take);
        return result;
    }

    /// <summary>
    /// Retrieves count of requests grouped by real estate object type.
    /// </summary>
    [HttpGet("requests-by-object")]
    public async Task<ActionResult<List<RealEstateTypeRequestCountDto>>> GetRequestCountByObject()
    {
        var result = await analyticsService.GetRequestCountByObjectTypeAsync();
        if (result == null || result.Count == 0)
            return NotFound("No request counts found.");

        return result;
    }

    /// <summary>
    /// Retrieves clients having requests with minimum price.
    /// </summary>
    [HttpGet("clients-with-min-price")]
    public async Task<ActionResult<List<ClientsWithMinPriceDto>>> GetClientsWithMinPriceRequests()
    {
        var result = await analyticsService.GetClientsWithMinPriceRequestsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves clients grouped by selected estate type.
    /// </summary>
    /// <param name="type">Type of real estate</param>
    [HttpGet("buyers-by-estate-type")]
    public async Task<ActionResult<List<CounterpartyDto>>> GetClientsByEstateType([FromQuery] string type)
    {
        if (!Enum.TryParse<RealEstateType>(type, true, out var estateType))
        {
            var validTypes = string.Join(", ", Enum.GetNames(typeof(RealEstateType)));
            return BadRequest($"Invalid RealEstateType: {type}. Valid values are: {validTypes}");
        }

        var result = await analyticsService.GetClientsByEstateTypeAsync(estateType);
        return Ok(result);
    }
}