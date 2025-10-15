namespace EstateAgency.Domain.Interfaces;

/// <summary>
/// Generic repository interface for basic CRUD operations with any entity type.
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously add an entity.
    /// </summary>
    public Task AddAsync(T entity);

    /// <summary>
    /// Asynchronously delete an entity by identifier.
    /// </summary>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Asynchronously get all entities.
    /// </summary>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Asynchronously get an entity by identifier.
    /// </summary>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Check if an entity exists by the specified identifier.
    /// </summary>
    public Task<bool> IsExistsAsync(int id);

    /// <summary>
    /// Asynchronously update entity data.
    /// </summary>
    public Task UpdateAsync(T entity);
}