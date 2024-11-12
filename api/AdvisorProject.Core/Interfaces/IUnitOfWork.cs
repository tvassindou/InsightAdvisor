using AdvisorProject.Core.Entities;

namespace AdvisorProject.Core.Interfaces;

/// <summary>
/// Interface for managing unit of work, coordinating repositories, and handling transactions across multiple data operations.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for <see cref="Advisor"/> entities.
    /// </summary>
    IRepository<Advisor> Advisors { get; }

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of beginning a transaction.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current database transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> CommitAsync();

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Rolls back the current database transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of rolling back a transaction.</returns>
    Task RollbackAsync();
}
