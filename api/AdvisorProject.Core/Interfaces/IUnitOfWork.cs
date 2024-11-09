
using AdvisorProject.Core.Entities;

namespace AdvisorProject.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<Advisor> Advisors { get; }
    Task BeginTransactionAsync();
    Task<int> CommitAsync();

    Task<int> SaveChangesAsync();

    Task RollbackAsync();
}
