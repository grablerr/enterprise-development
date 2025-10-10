using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing RealEstate entities using Entity Framework Core.
/// Supports asynchronous CRUD operations to add, delete, retrieve, update, and check existence.
/// </summary>
public class RealEstateRepository(AppDbContext context) : IRealEstateRepository
{
    /// <summary>
    /// Adds a new RealEstate entity asynchronously.
    /// </summary>
    /// <param name="realEstate">Entity to add</param>
    public async Task AddAsync(RealEstate realEstate)
    {
        if (realEstate == null) throw new ArgumentNullException(nameof(realEstate));

        await context.RealEstates.AddAsync(realEstate);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a RealEstate entity by its ID asynchronously.
    /// Throws KeyNotFoundException if entity not found.
    /// </summary>
    /// <param name="id">ID of the entity to delete</param>
    public async Task DeleteAsync(int id)
    {
        var realEstate = await context.RealEstates.FindAsync(id);
        if (realEstate == null) throw new KeyNotFoundException($"Estate with Id {id} not found.");

        context.RealEstates.Remove(realEstate);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all RealEstate entities asynchronously.
    /// </summary>
    public async Task<IEnumerable<RealEstate>> GetAllAsync() =>
        await context.RealEstates.ToListAsync();

    /// <summary>
    /// Retrieves a RealEstate entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">ID of the entity to retrieve</param>
    public async Task<RealEstate> GetByIdAsync(int id) =>
        await context.RealEstates.FindAsync(id);

    /// <summary>
    /// Checks whether a RealEstate entity exists by its ID asynchronously.
    /// </summary>
    /// <param name="id">ID to check for existence</param>
    public async Task<bool> IsExistsAsync(int id) =>
        await context.RealEstates.AnyAsync(e => e.Id == id);

    /// <summary>
    /// Updates an existing RealEstate entity asynchronously.
    /// Throws KeyNotFoundException if entity not found.
    /// </summary>
    /// <param name="realEstate">Entity with updated data</param>
    public async Task UpdateAsync(RealEstate realEstate)
    {
        if (realEstate == null) throw new ArgumentNullException(nameof(realEstate));
        var toUpdate = await context.RealEstates.FindAsync(realEstate.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Estate with Id {realEstate.Id} not found.");

        toUpdate.Type = realEstate.Type;
        toUpdate.Purpose = realEstate.Purpose;
        toUpdate.CadastralNumber = realEstate.CadastralNumber;
        toUpdate.Address = realEstate.Address;
        toUpdate.FloorNumber = realEstate.FloorNumber;
        toUpdate.Floors = realEstate.Floors;
        toUpdate.Square = realEstate.Square;
        toUpdate.Rooms = realEstate.Rooms;
        toUpdate.CeilingHeight = realEstate.CeilingHeight;
        toUpdate.IsEncumbrance = realEstate.IsEncumbrance;

        context.RealEstates.Update(toUpdate);
        await context.SaveChangesAsync();
    }
}