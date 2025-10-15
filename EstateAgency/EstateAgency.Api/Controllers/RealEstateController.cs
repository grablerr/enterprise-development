using EstateAgency.Application.Dtos;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing real estate entities.
/// </summary>
[ApiController]
[Route("api/real-estates")]
public class RealEstateController(IRepository<RealEstate> realEstateRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all real estate records asynchronously.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RealEstateDto>>> GetAllRealEstates()
    {
        var realEstates = await realEstateRepository.GetAllAsync();

        var dtoList = realEstates.Select(e => new RealEstateDto
        {
            Id = e.Id,
            Type = e.Type.ToString(),
            Purpose = e.Purpose.ToString(),
            CadastralNumber = e.CadastralNumber,
            Address = e.Address,
            FloorNumber = e.FloorNumber,
            Floors = e.Floors,
            Square = e.Square,
            Rooms = e.Rooms,
            CeilingHeight = e.CeilingHeight,
            IsEncumbrance = e.IsEncumbrance,
        }).ToList();

        return Ok(dtoList);
    }


    /// <summary>
    /// Retrieves a real estate record by ID. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RealEstateDto>> GetRealEstateById(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) return NotFound();

        var dto = new RealEstateDto
        {
            Id = realEstate.Id,
            Type = realEstate.Type.ToString(),
            Purpose = realEstate.Purpose.ToString(),
            CadastralNumber = realEstate.CadastralNumber,
            Address = realEstate.Address,
            FloorNumber = realEstate.FloorNumber,
            Floors = realEstate.Floors,
            Square = realEstate.Square,
            Rooms = realEstate.Rooms,
            CeilingHeight = realEstate.CeilingHeight,
            IsEncumbrance = realEstate.IsEncumbrance,
        };

        return Ok(dto);
    }

    /// <summary>
    /// Deletes a real estate record by ID. Returns 404 if record doesn't exist.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRealEstateById(int id)
    {
        var isExists = await realEstateRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await realEstateRepository.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new real estate entry. Validates the model state before adding.
    /// </summary>
    /// <param name="toCreateDto">Real estate entity</param>
    [HttpPost]
    public async Task<ActionResult<RealEstate>> CreateRealEstate([FromBody] RealEstateCreateDto toCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!Enum.TryParse<RealEstateType>(toCreateDto.Type, true, out var typeEnum))
            return BadRequest($"Invalid RealEstateType value: {toCreateDto.Type}");

        if (!Enum.TryParse<RealEstatePurpose>(toCreateDto.Purpose, true, out var purposeEnum))
            return BadRequest($"Invalid RealEstatePurpose value: {toCreateDto.Purpose}");

        var realEstate = new RealEstate
        {
            Type = typeEnum,
            Purpose = purposeEnum,
            CadastralNumber = toCreateDto.CadastralNumber,
            Address = toCreateDto.Address,
            FloorNumber = toCreateDto.FloorNumber,
            Floors = toCreateDto.Floors,
            Square = toCreateDto.Square,
            Rooms = toCreateDto.Rooms,
            CeilingHeight = toCreateDto.CeilingHeight,
            IsEncumbrance = toCreateDto.IsEncumbrance,
        };

        await realEstateRepository.AddAsync(realEstate);
        return CreatedAtAction(nameof(GetRealEstateById), new { id = realEstate.Id }, realEstate);
    }

    /// <summary>
    /// Updates an existing real estate record. Validates input and checks if the record exists. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    /// <param name="updDto">Updated real estate data</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateRealEstate(int id, [FromBody] RealEstateCreateDto updDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var old = await realEstateRepository.GetByIdAsync(id);
        if (old == null) return NotFound();

        if (!Enum.TryParse<RealEstateType>(updDto.Type, true, out var typeEnum))
            return BadRequest($"Invalid RealEstateType: {updDto.Type}");

        if (!Enum.TryParse<RealEstatePurpose>(updDto.Purpose, true, out var purposeEnum))
            return BadRequest($"Invalid RealEstatePurpose: {updDto.Purpose}");

        old.Type = typeEnum;
        old.Purpose = purposeEnum;
        old.CadastralNumber = updDto.CadastralNumber;
        old.Address = updDto.Address;
        old.FloorNumber = updDto.FloorNumber;
        old.Floors = updDto.Floors;
        old.Square = updDto.Square;
        old.Rooms = updDto.Rooms;
        old.CeilingHeight = updDto.CeilingHeight;
        old.IsEncumbrance = updDto.IsEncumbrance;

        await realEstateRepository.UpdateAsync(old);

        return NoContent();
    }

}