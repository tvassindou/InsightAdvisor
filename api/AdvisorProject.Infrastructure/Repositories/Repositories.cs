using AdvisorProject.Core.Entities;
using AdvisorProject.Core.Interfaces;
using AdvisorProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AdvisorProject.Infrastructure.Repositories;
public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly AdvisorDbContext _context;

    public Repository(AdvisorDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }


    public IQueryable<T> GetAll()
    {
        return  _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

}
