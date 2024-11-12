using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvisorProject.Controllers;

/// <summary>
/// Controller for managing advisor entities, including operations for retrieving, updating, and deleting advisors.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class AdvisorsController : ControllerBase
{
    private readonly ILogger<AdvisorsController> _logger;
    private readonly IAdvisorService _advisorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdvisorsController"/> class.
    /// </summary>
    /// <param name="advisorService">The service for managing advisors.</param>
    /// <param name="logger">The logger instance.</param>
    public AdvisorsController(IAdvisorService advisorService, ILogger<AdvisorsController> logger)
    {
        _advisorService = advisorService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves an advisor by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor.</param>
    /// <returns>An advisor's details if found, or a <see cref="NotFoundResult"/> if not found.</returns>
    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<AdvisorDto>> GetAdvisorById(int id)
    {
        var advisor = await _advisorService.GetAdvisorByIdAsync(id);
        if (advisor == null)
        {
            _logger.LogInformation("Advisor not found.");
            return NotFound();
        }

        return Ok(advisor);
    }

    /// <summary>
    /// Retrieves a paginated list of advisors with an optional filter.
    /// </summary>
    /// <param name="filter">The filter to search by full name (optional).</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of advisors.</returns>
    [HttpGet("GetAll/{page}/{pageSize}")]
    public async Task<ActionResult<PagedResult<AdvisorDto>>> GetAllAdvisors(string filter = "", int page = 0, int pageSize = 10)
    {
        var advisors = await _advisorService.GetPagedAdvisorsAsync(page, pageSize, filter);
        if (advisors == null)
        {
            _logger.LogInformation("No advisors found.");
            return NotFound();
        }
        return Ok(advisors);
    }

    /// <summary>
    /// Updates an existing advisor's information.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to update.</param>
    /// <param name="updateDto">The updated advisor details.</param>
    /// <returns>A <see cref="OkResult"/> if update is successful, or <see cref="NotFoundResult"/> if the advisor does not exist.</returns>
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateAdvisor(int id, [FromBody] UpdateAdvisorDto updateDto)
    {
        // Check if the incoming model is valid
        if (!ModelState.IsValid)
        {
            // Return a BadRequest response with validation errors
            return BadRequest(ModelState);
        }

        var success = await _advisorService.UpdateAdvisorAsync(id, updateDto);
        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }

    /// <summary>
    /// Deletes an advisor by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to delete.</param>
    /// <returns>A <see cref="OkResult"/> if deletion is successful, or <see cref="NotFoundResult"/> if the advisor does not exist.</returns>
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAdvisor(int id)
    {
        var success = await _advisorService.DeleteAdvisorAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return Ok();
    }
}
