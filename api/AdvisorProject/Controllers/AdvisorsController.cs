using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvisorProject.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class AdvisorsController : ControllerBase
{
    private readonly ILogger<AdvisorsController> _logger;
    private readonly IAdvisorService _advisorService;

    public AdvisorsController(IAdvisorService advisorService, ILogger<AdvisorsController> logger)
    {
        _advisorService = advisorService;
        _logger = logger;
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<AdvisorDto>> GetAdvisorById(int id)
    {
        var advisor = await _advisorService.GetAdvisorByIdAsync(id);
        if (advisor == null)
        {
            _logger.LogInformation("advisor is null");
            return NotFound();
        }

        return Ok(advisor);
    }

    [HttpGet("GetAll/{page}/{pageSize}")]
    public async Task<ActionResult<AdvisorDto>> GetAllAdvisors(string filter = "", int page = 0, int pageSize = 10)
    {
        var advisors = await _advisorService.GetPagedAdvisorsAsync(page, pageSize, filter);
        if (advisors == null)
        {
            _logger.LogInformation("advisors is null");
            return NotFound();
        }
        return Ok(advisors);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateAdvisor(int id, [FromBody] UpdateAdvisorDto updateDto)
    {
        var success = await _advisorService.UpdateAdvisorAsync(id, updateDto);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAdvisor(int id)
    {
        var success = await _advisorService.DeleteAdvisorAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
