using Microsoft.AspNetCore.Mvc;
using Application.AnalyticService;
using EstateAgency.Domain.Enums;

namespace Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("sellers-by-period")]
    public async Task<IActionResult> GetSellersByPeriod()
    {
        var result = await analyticsService.GetSellersByPeriodAsync();
        return Ok(result);
    }

    [HttpGet("top-clients")]
    public async Task<IActionResult> GetTopClientsByRequests(
        [FromQuery] ApplicationType type,
        [FromQuery] int take = 5)
    {
        var result = await analyticsService.GetTopClients5ByRequestsAsync(type, take);
        return Ok(result);
    }

    [HttpGet("requests-by-object")]
    public async Task<IActionResult> GetRequestCountByObject()
    {
        var result = await analyticsService.GetRequestCountByObjectTypeAsync();
        return Ok(result);
    }

    [HttpGet("clients-with-min-price")]
    public async Task<IActionResult> GetClientsWithMinPriceRequests()
    {
        var result = await analyticsService.GetClientsWithMinPriceRequestsAsync();
        return Ok(result);
    }

    [HttpGet("buyers-by-estate-type")]
    public async Task<IActionResult> GetClientsByEstateType([FromQuery] RealEstateType type)
    {
        var result = await analyticsService.GetClientsByEstateTypeAsync(type);
        return Ok(result);
    }
}
