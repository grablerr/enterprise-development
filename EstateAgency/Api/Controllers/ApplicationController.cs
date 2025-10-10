using Microsoft.AspNetCore.Mvc;
using EstateAgency.Domain.Interfaces;

namespace Api.Controllers;

/// <summary>
/// Controller for managing real estate applications.
/// </summary>
[ApiController]
[Route("api/applications")]
public class ApplicationController(
    IApplicationRepository applicationRepository,
    IRealEstateRepository realEstateRepository,
    ICounterpartyRepository counterpartyRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all applications asynchronously.
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> GetAllApplications()
    {
        var applications = await applicationRepository.GetAllAsync();
        return Ok(applications);
    }

    /// <summary>
    /// Retrieves an application by its ID.
    /// Returns 404 if not found.
    /// Returns 409 if associated real estate or counterparty is missing.
    /// </summary>
    /// <param name="id">Application identifier</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var application = await applicationRepository.GetByIdAsync(id);
        if (application == null)
            return NotFound();

        var realEstate = await realEstateRepository.GetByIdAsync(application.RealEstateId);
        var counterparty = await counterpartyRepository.GetByIdAsync(application.CounterpartyId);
        if (realEstate == null || counterparty == null)
            return Conflict("realEstate or Counterparty not found");

        return Ok(application);
    }

    /// <summary>
    /// Deletes an application by its ID.
    /// Returns 404 if not found.
    /// </summary>
    /// <param name="id">Application identifier</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteApplicationById(int id)
    {
        var isExists = await applicationRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await applicationRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new application.
    /// Validates the input model and existence of related real estate and counterparty.
    /// </summary>
    /// <param name="toCreate">Application entity to create</param>
    [HttpPost("")]
    public async Task<IActionResult> CreateApplication([FromBody] EstateAgency.Domain.Entities.Application toCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isRealEstateExists = await realEstateRepository.IsExistsAsync(toCreate.RealEstateId);
        var isCounterpartyExists = await counterpartyRepository.IsExistsAsync(toCreate.CounterpartyId);

        if (!isRealEstateExists || !isCounterpartyExists) return NotFound();

        await applicationRepository.AddAsync(toCreate);
        return CreatedAtAction(nameof(GetApplicationById), new { id = toCreate.Id }, toCreate);
    }

    /// <summary>
    /// Updates an existing application by ID.
    /// Validates the input model and existence of related entities.
    /// Returns 404 if application to update does not exist.
    /// </summary>
    /// <param name="id">Application identifier</param>
    /// <param name="upd">Updated application entity</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateApplication(int id, [FromBody] EstateAgency.Domain.Entities.Application upd)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isRealEstateExists = await realEstateRepository.IsExistsAsync(upd.RealEstateId);
        var isCounterpartyExists = await counterpartyRepository.IsExistsAsync(upd.CounterpartyId);

        if (!isRealEstateExists || !isCounterpartyExists) return NotFound();

        var old = await applicationRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        old.Id = upd.Id;
        old.CounterpartyId = upd.CounterpartyId;
        old.RealEstateId = upd.RealEstateId;
        old.TransactionAmount = upd.TransactionAmount;
        old.Type = upd.Type;
        old.Date = upd.Date;
        await applicationRepository.UpdateAsync(old);

        return NoContent();
    }
};