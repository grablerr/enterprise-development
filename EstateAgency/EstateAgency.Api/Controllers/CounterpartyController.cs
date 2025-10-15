using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing counterparties.
/// </summary>
[ApiController]
[Route("api/counterparties")]
public class CounterpartyController(IRepository<Counterparty> counterpartyRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all counterparties asynchronously.
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> GetAllConterparties()
    {
        var conterparties = await counterpartyRepository.GetAllAsync();
        return Ok(conterparties);
    }

    /// <summary>
    /// Retrieves a counterparty by ID.
    /// Returns 404 if not found.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCounterpartyById(int id)
    {
        var conterparty = await counterpartyRepository.GetByIdAsync(id);
        if (conterparty == null) return NotFound();

        return Ok(conterparty);
    }

    /// <summary>
    /// Deletes a counterparty by ID.
    /// Returns 404 if not found.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCounterpartyById(int id)
    {
        var isExists = await counterpartyRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await counterpartyRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new counterparty.
    /// Validates the input model.
    /// </summary>
    /// <param name="toCreate">Counterparty entity to create</param>
    [HttpPost("")]
    public async Task<IActionResult> CreateCounterparty([FromBody] Counterparty toCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await counterpartyRepository.AddAsync(toCreate);
        return CreatedAtAction(nameof(GetCounterpartyById), new { id = toCreate.Id }, toCreate);
    }

    /// <summary>
    /// Updates an existing counterparty by ID.
    /// Validates the input model.
    /// Returns 404 if counterparty does not exist.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    /// <param name="upd">Updated counterparty entity</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCounterparty(int id, [FromBody] Counterparty upd)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var old = await counterpartyRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        old.Id = upd.Id;
        old.FullName = upd.FullName;
        old.PassportNumber = upd.PassportNumber;
        old.PhoneNumber = upd.PhoneNumber;
        await counterpartyRepository.UpdateAsync(old);

        return NoContent();
    }
}
