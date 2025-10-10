using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;

/// <summary>
/// Repository interface for managing RealEstate entities.
/// Defines asynchronous methods for CRUD operations and existence checking.
/// </summary>
public interface IRealEstateRepository
{
    /// <summary>
    /// Adds a new RealEstate entity asynchronously.
    /// </summary>
    /// <param name="realEstate">RealEstate entity to add</param>
    public Task AddAsync(RealEstate realEstate);

    /// <summary>
    /// Deletes a RealEstate entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the RealEstate to delete</param>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves all RealEstate entities asynchronously.
    /// </summary>
    public Task<IEnumerable<RealEstate>> GetAllAsync();

    /// <summary>
    /// Retrieves a RealEstate entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the RealEstate to retrieve</param>
    public Task<RealEstate> GetByIdAsync(int id);

    /// <summary>
    /// Checks whether a RealEstate entity exists by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the RealEstate to check</param>
    public Task<bool> IsExistsAsync(int id);

    /// <summary>
    /// Updates an existing RealEstate entity asynchronously.
    /// </summary>
    /// <param name="realEstate">RealEstate entity with updated data</param>
    public Task UpdateAsync(RealEstate realEstate);
}