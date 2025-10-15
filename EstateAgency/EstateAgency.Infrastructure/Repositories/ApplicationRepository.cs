using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstateAgency.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing Application entities using Entity Framework Core.
/// Provides asynchronous methods to add, delete, retrieve, update, and check existence of Application entities.
/// </summary>
public class ApplicationRepository(AppDbContext context) : IRepository<Application>
{
    /// <summary>
    /// Adds a new Application to the database asynchronously.
    /// </summary>
    /// <param name="application">Application entity to add</param>
    public async Task AddAsync(Application application)
    {
        await context.Applications.AddAsync(application);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an Application by ID asynchronously.
    /// Throws KeyNotFoundException if the entity does not exist.
    /// </summary>
    /// <param name="id">ID of the Application to delete</param>
    public async Task DeleteAsync(int id)
    {
        var application = await context.Applications.FindAsync(id) ?? throw new KeyNotFoundException($"Estate with Id {id} not found.");

        context.Applications.Remove(application);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all Application entities asynchronously.
    /// </summary>
    public async Task<IEnumerable<Application>> GetAllAsync() =>
        await context.Applications.ToListAsync();

    /// <summary>
    /// Retrieves an Application entity by ID asynchronously.
    /// </summary>
    /// <param name="id">ID of the Application to retrieve</param>
    public async Task<Application?> GetByIdAsync(int id) =>
        await context.Applications.FindAsync(id);

    /// <summary>
    /// Checks if an Application entity exists by ID asynchronously.
    /// </summary>
    /// <param name="id">ID of the Application to check</param>
    public async Task<bool> IsExistsAsync(int id) =>
        await context.Applications.AnyAsync(a => a.Id == id);

    /// <summary>
    /// Updates an existing Application entity asynchronously.
    /// Throws KeyNotFoundException if the entity does not exist.
    /// </summary>
    /// <param name="application">Application entity with updated data</param>
    public async Task UpdateAsync(Application application)
    {
        var toUpdate = await context.Applications.FindAsync(application.Id) ?? throw new KeyNotFoundException($"Estate with Id {application.Id} not found.");

        toUpdate.CounterpartyId = application.CounterpartyId;
        toUpdate.RealEstateId = application.RealEstateId;
        toUpdate.TransactionAmount = application.TransactionAmount;
        toUpdate.Type = application.Type;
        toUpdate.Date = application.Date;

        context.Applications.Update(toUpdate);
        await context.SaveChangesAsync();
    }
}