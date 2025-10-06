using Microsoft.AspNetCore.Mvc;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;


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

        upd.Id = old.Id;
        await realEstateRepository.UpdateAsync(old);

        return NoContent();
    }
}