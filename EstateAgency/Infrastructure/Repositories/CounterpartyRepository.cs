using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing Counterparty entities using Entity Framework Core.
/// Provides asynchronous CRUD operations to add, delete, retrieve, update and check existence of Counterparty entities.
/// </summary>
public class CounterpartyRepository(AppDbContext context) : ICounterpartyRepository
{
    /// <summary>
    /// Adds a new Counterparty entity asynchronously.
    /// </summary>
    /// <param name="counterparty">Counterparty entity to add</param>
    public async Task AddAsync(Counterparty counterparty)
    {
        if (counterparty == null) throw new ArgumentNullException(nameof(counterparty));

        await context.Counterparties.AddAsync(counterparty);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a Counterparty entity by ID asynchronously.
    /// Throws KeyNotFoundException if the entity is not found.
    /// </summary>
    /// <param name="id">ID of the Counterparty to delete</param>
    public async Task DeleteAsync(int id)
    {
        var counterparty = await context.Counterparties.FindAsync(id);
        if (counterparty == null) throw new KeyNotFoundException($"Counterparty with Id {id} not found.");

        context.Counterparties.Remove(counterparty);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all Counterparty entities asynchronously.
    /// </summary>
    public async Task<IEnumerable<Counterparty>> GetAllAsync() =>
        await context.Counterparties.ToListAsync();

    /// <summary>
    /// Retrieves a Counterparty entity by ID asynchronously.
    /// </summary>
    /// <param name="id">ID of the Counterparty to retrieve</param>
    public async Task<Counterparty> GetByIdAsync(int id) =>
        await context.Counterparties.FindAsync(id);

    /// <summary>
    /// Checks whether a Counterparty entity exists by ID asynchronously.
    /// </summary>
    /// <param name="id">ID of the Counterparty to check</param>
    public async Task<bool> IsExistsAsync(int id) =>
        await context.Counterparties.AnyAsync(c => c.Id == id);

    /// <summary>
    /// Updates an existing Counterparty entity asynchronously.
    /// Throws KeyNotFoundException if the entity is not found.
    /// </summary>
    /// <param name="counterparty">Counterparty entity with updated data</param>
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