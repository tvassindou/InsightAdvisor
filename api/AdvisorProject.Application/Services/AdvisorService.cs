using AdvisorProject.Application.DTOs;
using AdvisorProject.Application.Interfaces;
using AdvisorProject.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvisorProject.Application.Services;
public class AdvisorService : BaseService, IAdvisorService
{
    public AdvisorService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public async Task<AdvisorDto> GetAdvisorByIdAsync(int id)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        return Mapper.Map<AdvisorDto>(advisor);
    }

    public async Task<PagedResult<AdvisorDto>> GetPagedAdvisorsAsync(int page, int pageSize, string filter)
    {
        var query =  UnitOfWork.Advisors.GetAll().WhereIf(!string.IsNullOrEmpty(filter),a=>a.FullName.Contains(filter));
    
        var totalRecords = await query.CountAsync();
        var advisors = await query.OrderBy(x=>x.FullName).PageBy(page, pageSize)
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

    public async Task<bool> UpdateAdvisorAsync(int id, UpdateAdvisorDto updateDto)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        if (advisor == null) return false;

        Mapper.Map(updateDto, advisor); // Met à jour les propriétés de l'entité avec le DTO
        UnitOfWork.Advisors.Update(advisor);
        await UnitOfWork.SaveChangesAsync(); // Enregistre les modifications dans la base de données

        return true;
    }

    public async Task<bool> DeleteAdvisorAsync(int id)
    {
        var advisor = await UnitOfWork.Advisors.GetByIdAsync(id);
        if (advisor == null) return false;

        UnitOfWork.Advisors.Delete(advisor);
        await UnitOfWork.SaveChangesAsync(); // Enregistre la suppression dans la base de données

        return true;
    }
}