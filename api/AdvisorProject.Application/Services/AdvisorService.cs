using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Interfaces;
using AdvisorProject.Core.Entities;
using AdvisorProject.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvisorProject.Application.Services;

/// <summary>
/// Service for managing operations related to <see cref="Advisor"/> entities, such as creating, updating, deleting, and retrieving advisor data.
/// </summary>
public class AdvisorService : BaseService, IAdvisorService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvisorService"/> class.
    /// </summary>
    /// <param name="unitOfWork">An instance of <see cref="IUnitOfWork"/> for database operations.</param>
    /// <param name="mapper">An instance of <see cref="IMapper"/> for mapping DTOs and entities.</param>
    public AdvisorService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    /// <summary>
    /// Retrieves an advisor by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to retrieve.</param>
    /// <returns>An <see cref="AdvisorDto"/> containing advisor data, or <c>null</c> if the advisor is not found.</returns>
    public async Task<AdvisorDto> GetAdvisorByIdAsync(int id)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        return Mapper.Map<AdvisorDto>(advisor);
    }

    /// <summary>
    /// Retrieves a paginated list of advisors with an optional filter.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of advisors per page.</param>
    /// <param name="filter">An optional filter to search advisors by their full name.</param>
    /// <returns>A <see cref="PagedResult{AdvisorDto}"/> containing the paginated list of advisors.</returns>
    public async Task<PagedResult<AdvisorDto>> GetPagedAdvisorsAsync(int page, int pageSize, string filter)
    {
        var query = UnitOfWork.Advisors.GetAll().WhereIf(!string.IsNullOrEmpty(filter), a => a.FullName.Contains(filter));

        var totalRecords = await query.CountAsync();
        var advisors = await query.OrderBy(x => x.Id).PageBy(page, pageSize)
            .ToListAsync();

        var advisorDtos = Mapper.Map<IEnumerable<AdvisorDto>>(advisors);

        var pagedResult = new PagedResult<AdvisorDto>
        {
            Items = advisorDtos,
            TotalItems = totalRecords,
            PageNumber = page,
            PageSize = pageSize
        };

        return pagedResult;
    }

    /// <summary>
    /// Creates a new advisor in the system.
    /// </summary>
    /// <param name="createDto">A <see cref="CreateAdvisorDto"/> containing the data to create the advisor.</param>
    /// <returns><c>true</c> if the creation is successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> CreateAdvisorAsync(CreateAdvisorDto createDto)
    {
        var advisor = Mapper.Map<Advisor>(createDto);
        advisor.HealthStatus = GenerateRandomHealthStatus();
        await UnitOfWork.Advisors.AddAsync(advisor);

        var result = await UnitOfWork.SaveChangesAsync();

        return result > 0;
    }

    /// <summary>
    /// Updates an existing advisor's information.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to update.</param>
    /// <param name="updateDto">A <see cref="UpdateAdvisorDto"/> containing the updated advisor data.</param>
    /// <returns><c>true</c> if the update is successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> UpdateAdvisorAsync(int id, UpdateAdvisorDto updateDto)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        if (advisor == null) return false;

        Mapper.Map(updateDto, advisor);
        UnitOfWork.Advisors.Update(advisor);
        await UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Deletes an advisor from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the advisor to delete.</param>
    /// <returns><c>true</c> if the deletion is successful; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteAdvisorAsync(int id)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        if (advisor == null) return false;

        UnitOfWork.Advisors.Delete(advisor);
        await UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Generates a random health status value between 0 and 1.
    /// </summary>
    /// <returns>A double representing the health status, ranging from 0 to 1.</returns>
    private double GenerateRandomHealthStatus()
    {
        var random = new Random();
        return random.NextDouble();
    }
}