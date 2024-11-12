using AdvisorProject.Core.Interfaces;
using AutoMapper;

namespace AdvisorProject.Application.Services;

/// <summary>
/// Abstract base class for service classes, providing access to shared dependencies such as
/// <see cref="IUnitOfWork"/> for database operations and <see cref="IMapper"/> for object mapping.
/// </summary>
public abstract class BaseService
{
    /// <summary>
    /// Gets the unit of work for managing data operations across repositories.
    /// </summary>
    protected IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Gets the mapper for converting between entities and data transfer objects (DTOs).
    /// </summary>
    protected IMapper Mapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService"/> class with the specified
    /// unit of work and mapper.
    /// </summary>
    /// <param name="unitOfWork">An instance of <see cref="IUnitOfWork"/> to manage repository transactions.</param>
    /// <param name="mapper">An instance of <see cref="IMapper"/> for mapping objects between types.</param>
    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
}
