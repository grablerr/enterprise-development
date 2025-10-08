using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/counterparties")]
public class CounterpartyController(ICounterpartyRepository counterpartyRepository) : ControllerBase


{
    [HttpGet("")]
    public async Task<IActionResult> GetAllConterparties()
    {
        var conterparties = await counterpartyRepository.GetAllAsync();
        return Ok(conterparties);

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCounterpartyById(int id)
    {
        var conterparty = await counterpartyRepository.GetByIdAsync(id);
        if (conterparty == null) return NotFound();

        return Ok(conterparty);

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCounterpartyById(int id)
    {
        var isExists = await counterpartyRepository.IsExistsAsync(id);
        if (!isExists) return NotFound();

        await counterpartyRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateCounterparty([FromBody] Counterparty toCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        await counterpartyRepository.AddAsync(toCreate);
        return CreatedAtAction(nameof(GetCounterpartyById), new { id = toCreate.Id }, toCreate);
    }

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
