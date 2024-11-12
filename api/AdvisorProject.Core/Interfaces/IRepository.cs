using AdvisorProject.Core.Entities;

namespace AdvisorProject.Core.Interfaces;

/// <summary>
/// Generic repository interface for handling data access operations for entities of type <see cref="EntityBase"/>.
/// Provides methods for CRUD operations and data retrieval.
/// </summary>
/// <typeparam name="T">The entity type that implements <see cref="EntityBase"/>.</typeparam>
public interface IRepository<T> where T : EntityBase
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities as an <see cref="IQueryable{T}"/> to support deferred execution and further querying.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> of all entities.</returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Retrieves all entities asynchronously as an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);
}
