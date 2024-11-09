using AdvisorProject.Core.Interfaces;
using AutoMapper;

namespace AdvisorProject.Application.Services;
public abstract class BaseService
{
    protected IUnitOfWork UnitOfWork { get; }
    protected IMapper Mapper { get; }

    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
}