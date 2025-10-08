using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;


namespace Api.Controllers;
[ApiController]
[Route("api/estates")]
public class RealEstateController(IRealEstateRepository realEstateRepository) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllRealEstates()
    {
        var realEstates = await realEstateRepository.GetAllAsync();
        return Ok(realEstates);

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRealEstateById(int id)
    {
        var realEstate = await realEstateRepository.GetByIdAsync(id);
        if (realEstate == null) return NotFound();

        return Ok(realEstate);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRealEstateById(int id)
    {
        var isExists = await realEstateRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await realEstateRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateRealEstate([FromBody] RealEstate toCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        await realEstateRepository.AddAsync(toCreate);
        return CreatedAtAction(nameof(GetRealEstateById), new { id = toCreate.Id }, toCreate);
    }

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