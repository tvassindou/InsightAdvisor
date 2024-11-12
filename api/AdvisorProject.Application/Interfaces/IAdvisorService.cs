using AdvisorProject.Application.DTOs;

namespace AdvisorProject.Application.Interfaces;

/// <summary>
/// Interface for managing advisor operations such as retrieving, updating, and deleting advisor data.
/// </summary>
public interface IAdvisorService
{
    /// <summary>
    /// Retrieves an advisor by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to retrieve.</param>
    /// <returns>An <see cref="AdvisorDto"/> containing the advisor's data, or <c>null</c> if the advisor is not found.</returns>
    Task<AdvisorDto> GetAdvisorByIdAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of advisors, with an optional filter to search by full name.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of advisors to retrieve per page.</param>
    /// <param name="filter">An optional filter to search advisors by their full name. Default is an empty string.</param>
    /// <returns>A <see cref="PagedResult{AdvisorDto}"/> containing the paginated list of advisors.</returns>
    Task<PagedResult<AdvisorDto>> GetPagedAdvisorsAsync(int page, int pageSize, string filter = "");

    /// <summary>
    /// Updates an existing advisor's information.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to update.</param>
    /// <param name="updateDto">A <see cref="UpdateAdvisorDto"/> containing the updated advisor data.</param>
    /// <returns><c>true</c> if the update is successful; otherwise, <c>false</c>.</returns>
    Task<bool> UpdateAdvisorAsync(int id, UpdateAdvisorDto updateDto);

    /// <summary>
    /// Deletes an advisor by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to delete.</param>
    /// <returns><c>true</c> if the deletion is successful; otherwise, <c>false</c>.</returns>
    Task<bool> DeleteAdvisorAsync(int id);
}
