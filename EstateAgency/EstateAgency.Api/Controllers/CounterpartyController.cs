using EstateAgency.Application.Dtos;
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Counterparty>>> GetAllCounterparties()
    {
        var counterparties = await counterpartyRepository.GetAllAsync();
        return Ok(counterparties);
    }

    /// <summary>
    /// Retrieves a counterparty by ID. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Counterparty>> GetCounterpartyById(int id)
    {
        var counterparty = await counterpartyRepository.GetByIdAsync(id);
        if (counterparty == null) return NotFound();

        return Ok(counterparty);
    }

    /// <summary>
    /// Deletes a counterparty by ID. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCounterpartyById(int id)
    {
        var isExists = await counterpartyRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await counterpartyRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new counterparty. Validates the input model.
    /// </summary>
    /// <param name="toCreateDto">Counterparty entity to create</param>
    [HttpPost]
    public async Task<ActionResult<Counterparty>> CreateCounterparty([FromBody] CounterpartyCreateDto toCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var counterparty = new Counterparty
        {
            FullName = toCreateDto.FullName,
            PassportNumber = toCreateDto.PassportNumber,
            PhoneNumber = toCreateDto.PhoneNumber,
        };

        await counterpartyRepository.AddAsync(counterparty);
        return CreatedAtAction(nameof(GetCounterpartyById), new { id = counterparty.Id }, counterparty);
    }

    /// <summary>
    /// Updates an existing counterparty by ID. Validates the input model.
    /// Returns 404 if counterparty does not exist.
    /// </summary>
    /// <param name="id">Counterparty identifier</param>
    /// <param name="upd">Updated counterparty entity</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateCounterparty(int id, [FromBody] CounterpartyCreateDto upd)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var old = await counterpartyRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        old.FullName = upd.FullName;
        old.PassportNumber = upd.PassportNumber;
        old.PhoneNumber = upd.PhoneNumber;
        await counterpartyRepository.UpdateAsync(old);

        return NoContent();
    }
}