using AutoMapper;
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
public class RealEstateController(
    IRepository<RealEstate> realEstateRepository,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Retrieves all real estate records asynchronously.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RealEstateDto>>> GetAllRealEstates()
    {
        var realEstates = await realEstateRepository.GetAllAsync();

        var dtoList = mapper.Map<IEnumerable<RealEstateDto>>(realEstates);

        if (dtoList == null || !dtoList.Any())
            return NotFound("No real estates found.");

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

        var dto = mapper.Map<RealEstateDto>(realEstate);

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
    public async Task<ActionResult<RealEstateDto>> CreateRealEstate([FromBody] RealEstateCreateDto toCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!Enum.TryParse<RealEstateType>(toCreateDto.Type, true, out var typeEnum))
        {
            var validTypes = string.Join(", ", Enum.GetNames(typeof(RealEstateType)));
            return BadRequest($"Invalid RealEstateType: {toCreateDto.Type}. Valid values are: {validTypes}");
        }

        if (!Enum.TryParse<RealEstatePurpose>(toCreateDto.Purpose, true, out var purposeEnum))
        {
            var validPurposes = string.Join(", ", Enum.GetNames(typeof(RealEstatePurpose)));
            return BadRequest($"Invalid RealEstatePurpose: {toCreateDto.Purpose}. Valid values are: {validPurposes}");
        }

        var realEstate = mapper.Map<RealEstate>(toCreateDto);
        realEstate.Type = typeEnum;
        realEstate.Purpose = purposeEnum;

        await realEstateRepository.AddAsync(realEstate);

        var dto = mapper.Map<RealEstateDto>(realEstate);
        return CreatedAtAction(nameof(GetRealEstateById), new { id = realEstate.Id }, dto);
    }

    /// <summary>
    /// Updates an existing real estate record. Validates input and checks if the record exists. Returns 404 if not found.
    /// </summary>
    /// <param name="id">Real estate ID</param>
    /// <param name="updDto">Updated real estate data</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateRealEstate(int id, [FromBody] RealEstateCreateDto updDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var old = await realEstateRepository.GetByIdAsync(id);
        if (old == null)
            return NotFound();

        if (!Enum.TryParse<RealEstateType>(updDto.Type, true, out var typeEnum))
        {
            var validTypes = string.Join(", ", Enum.GetNames(typeof(RealEstateType)));
            return BadRequest($"Invalid RealEstateType: {updDto.Type}. Valid values are: {validTypes}");
        }

        if (!Enum.TryParse<RealEstatePurpose>(updDto.Purpose, true, out var purposeEnum))
        {
            var validPurposes = string.Join(", ", Enum.GetNames(typeof(RealEstatePurpose)));
            return BadRequest($"Invalid RealEstatePurpose: {updDto.Purpose}. Valid values are: {validPurposes}");
        }

        mapper.Map(updDto, old);
        old.Type = typeEnum;
        old.Purpose = purposeEnum;

        await realEstateRepository.UpdateAsync(old);

        return NoContent();
    }
}