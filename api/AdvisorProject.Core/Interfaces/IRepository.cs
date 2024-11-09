using AdvisorProject.Core.Entities;

namespace AdvisorProject.Core.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}