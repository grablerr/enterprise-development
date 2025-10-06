using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CounterpartyRepository(AppDbContext context) : ICounterpartyRepository
{
    public async Task AddAsync(Counterparty counterparty)
    {
        if (counterparty == null) throw new ArgumentNullException(nameof(counterparty));

        await context.Counterparties.AddAsync(counterparty);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var counterparty = await context.Counterparties.FindAsync(id);
        if (counterparty == null) throw new KeyNotFoundException($"Counterparty with Id {id} not found.");

        context.Counterparties.Remove(counterparty);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Counterparty>> GetAllAsync() =>
        await context.Counterparties.ToListAsync();

    public async Task<Counterparty> GetByIdAsync(int id) =>
        await context.Counterparties.FindAsync(id);

    public async Task<bool> IsExistsAsync(int id) =>
        await context.Counterparties.AnyAsync(c => c.Id == id);

    public async Task UpdateAsync(Counterparty counterparty)
    {
        if (counterparty == null) throw new ArgumentNullException(nameof(counterparty));
        var toUpdate = await context.Counterparties.FindAsync(counterparty.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Estate with Id {counterparty.Id} not found.");

        toUpdate.FullName = counterparty.FullName;
        toUpdate.PassportNumber = counterparty.PassportNumber;
        toUpdate.PhoneNumber = counterparty.PhoneNumber;

        context.Counterparties.Update(toUpdate);
        await context.SaveChangesAsync();

    }
}
