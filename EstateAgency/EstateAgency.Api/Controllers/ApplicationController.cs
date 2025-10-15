using EstateAgency.Application.Dtos;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing real estate applications.
/// </summary>
[ApiController]
[Route("api/applications")]
public class ApplicationController(
    IRepository<EstateAgency.Domain.Entities.Application> applicationRepository,
    IRepository<RealEstate> realEstateRepository,
    IRepository<Counterparty> counterpartyRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all applications asynchronously.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAllApplications()
    {
        var applications = await applicationRepository.GetAllAsync();

        var dtoList = applications.Select(a => new ApplicationDto
        {
            Id = a.Id,
            CounterpartyId = a.CounterpartyId,
            RealEstateId = a.RealEstateId,
            TransactionAmount = a.TransactionAmount,
            Type = a.Type.ToString(),
            Date = a.Date
        }).ToList();

        return Ok(dtoList);
    }

    /// <summary>
    /// Retrieves an application by its ID. Returns 404 if not found.
    /// Returns 409 if associated real estate or counterparty is missing.
    /// </summary>
    /// <param name="id">Application identifier</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(int id)
    {
        var application = await applicationRepository.GetByIdAsync(id);
        if (application == null)
            return NotFound();

        var realEstate = await realEstateRepository.GetByIdAsync(application.RealEstateId);
        var counterparty = await counterpartyRepository.GetByIdAsync(application.CounterpartyId);
        if (realEstate == null || counterparty == null)
            return Conflict("realEstate or Counterparty not found");

        var dto = new ApplicationDto
        {
            Id = application.Id,
            CounterpartyId = application.CounterpartyId,
            RealEstateId = application.RealEstateId,
            TransactionAmount = application.TransactionAmount,
            Type = application.Type.ToString(),
            Date = application.Date
        };

        return Ok(dto);
    }

    /// <summary>
    /// Deletes an application by its ID. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Application identifier</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteApplicationById(int id)
    {
        var isExists = await applicationRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await applicationRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new application. Validates the input model and existence of related real estate and counterparty.
    /// </summary>
    /// <param name="toCreateDto">Application entity to create</param>
    [HttpPost]
    public async Task<ActionResult<EstateAgency.Domain.Entities.Application>> CreateApplication([FromBody] ApplicationCreateDto toCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isRealEstateExists = await realEstateRepository.IsExistsAsync(toCreateDto.RealEstateId);
        var isCounterpartyExists = await counterpartyRepository.IsExistsAsync(toCreateDto.CounterpartyId);
        if (!isRealEstateExists || !isCounterpartyExists) return NotFound();

        if (!Enum.TryParse<ApplicationType>(toCreateDto.Type, true, out var typeEnum))
            return BadRequest($"Invalid ApplicationType: {toCreateDto.Type}");

        var application = new EstateAgency.Domain.Entities.Application
        {
            CounterpartyId = toCreateDto.CounterpartyId,
            RealEstateId = toCreateDto.RealEstateId,
            TransactionAmount = toCreateDto.TransactionAmount,
            Type = typeEnum,
            Date = toCreateDto.Date
        };

        await applicationRepository.AddAsync(application);

        return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
    }

    /// <summary>
    /// Updates an existing application by ID. Validates the input model and existence of related entities.
    /// Returns 404 if application to update does not exist.
    /// </summary>
    /// <param name="id">Application identifier</param>
    /// <param name="updDto">Updated application entity</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateApplication(int id, [FromBody] ApplicationCreateDto updDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!Enum.TryParse<ApplicationType>(updDto.Type, true, out var typeEnum))
            return BadRequest($"Invalid ApplicationType: {updDto.Type}");

        var isRealEstateExists = await realEstateRepository.IsExistsAsync(updDto.RealEstateId);
        var isCounterpartyExists = await counterpartyRepository.IsExistsAsync(updDto.CounterpartyId);

        if (!isRealEstateExists || !isCounterpartyExists) return NotFound();

        var old = await applicationRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        old.CounterpartyId = updDto.CounterpartyId;
        old.RealEstateId = updDto.RealEstateId;
        old.TransactionAmount = updDto.TransactionAmount;
        old.Type = typeEnum;
        old.Date = updDto.Date;

        await applicationRepository.UpdateAsync(old);

        return NoContent();
    }
};