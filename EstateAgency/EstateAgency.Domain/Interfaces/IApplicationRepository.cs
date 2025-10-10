using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;

/// <summary>
/// Repository interface for managing Application entities.
/// Defines asynchronous CRUD operations and existence checks.
/// </summary>
public interface IApplicationRepository
{
    /// <summary>
    /// Adds a new Application entity asynchronously.
    /// </summary>
    /// <param name="application">Application entity to add</param>
    public Task AddAsync(Application application);

    /// <summary>
    /// Deletes an Application entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Application to delete</param>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves all Application entities asynchronously.
    /// </summary>
    public Task<IEnumerable<Application>> GetAllAsync();

    /// <summary>
    /// Retrieves an Application entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Application to retrieve</param>
    public Task<Application> GetByIdAsync(int id);

    /// <summary>
    /// Checks whether an Application entity exists by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Identifier of the Application to check</param>
    public Task<bool> IsExistsAsync(int id);

    /// <summary>
    /// Updates an existing Application entity asynchronously.
    /// </summary>
    /// <param name="application">Application entity with updated data</param>
    public Task UpdateAsync(Application application);
}