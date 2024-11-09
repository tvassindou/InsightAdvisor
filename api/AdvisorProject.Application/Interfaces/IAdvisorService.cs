
using AdvisorProject.Application.DTOs;

namespace AdvisorProject.Application.Interfaces;
public interface IAdvisorService
{
    Task<AdvisorDto> GetAdvisorByIdAsync(int id);
    Task<PagedResult<AdvisorDto>> GetPagedAdvisorsAsync(int page, int pageSize, string filter = "");
    Task<bool> UpdateAdvisorAsync(int id, UpdateAdvisorDto updateDto);
    Task<bool> DeleteAdvisorAsync(int id);
}