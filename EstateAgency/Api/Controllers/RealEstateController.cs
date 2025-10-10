using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for managing real estate entities.
/// </summary>
[ApiController]
[Route("api/estates")]
public class RealEstateController(IRealEstateRepository realEstateRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all real estate records asynchronously.
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> GetAllRealEstates()
    {
        var realEstates = await realEstateRepository.GetAllAsync();
        return Ok(realEstates);

    }

    /// <summary>
    /// Retrieves a real estate record by ID.
    /// Returns 404 if not found.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRealEstateById(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) return NotFound();

        return Ok(realEstate);
    }

    /// <summary>
    /// Deletes a real estate record by ID.
    /// Returns 404 if record doesn't exist.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRealEstateById(int id)
    {
        var isExists = await realEstateRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await realEstateRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new real estate entry.
    /// Validates the model state before adding.
    /// </summary>
    /// <param name="toCreate">Real estate entity</param>
    [HttpPost("")]
    public async Task<IActionResult> CreateRealEstate([FromBody] RealEstate toCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await realEstateRepository.AddAsync(toCreate);
        return CreatedAtAction(nameof(GetRealEstateById), new { id = toCreate.Id }, toCreate);
    }

    /// <summary>
    /// Updates an existing real estate record.
    /// Validates input and checks if the record exists.
    /// Returns 404 if not found.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    /// <param name="upd">Updated real estate data</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRealEstate(int id, [FromBody] RealEstate upd)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var old = await realEstateRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        old.Id = upd.Id;
        old.Type = upd.Type;
        old.Purpose = upd.Purpose;
        old.CadastralNumber = upd.CadastralNumber;
        old.Address = upd.Address;
        old.FloorNumber = upd.FloorNumber;
        old.Floors = upd.Floors;
        old.Square = upd.Square;
        old.Rooms = upd.Rooms;
        old.CeilingHeight = upd.CeilingHeight;
        old.IsEncumbrance = upd.IsEncumbrance;
        await realEstateRepository.UpdateAsync(old);

        return NoContent();
    }
}