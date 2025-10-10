using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;

/// <summary>
/// Repository interface for managing Counterparty entities.
/// Defines asynchronous CRUD operations and existence check methods.
/// </summary>
public interface ICounterpartyRepository
{
    /// <summary>
    /// Adds a new Counterparty entity asynchronously.
    /// </summary>
    /// <param name="counterparty">Counterparty entity to add</param>
    public Task AddAsync(Counterparty counterparty);

    /// <summary>
    /// Deletes a Counterparty entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Counterparty to delete</param>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves all Counterparty entities asynchronously.
    /// </summary>
    public Task<IEnumerable<Counterparty>> GetAllAsync();

    /// <summary>
    /// Retrieves a Counterparty entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Counterparty to retrieve</param>
    public Task<Counterparty> GetByIdAsync(int id);

    /// <summary>
    /// Checks whether a Counterparty entity exists by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Counterparty to check</param>
    public Task<bool> IsExistsAsync(int id);

    /// <summary>
    /// Updates an existing Counterparty entity asynchronously.
    /// </summary>
    /// <param name="counterparty">Counterparty entity with updated data</param>
    public Task UpdateAsync(Counterparty counterparty);
}