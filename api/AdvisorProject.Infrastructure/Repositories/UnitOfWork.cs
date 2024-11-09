using AdvisorProject.Core.Entities;
using AdvisorProject.Core.Interfaces;
using AdvisorProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace AdvisorProject.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    #region Fields

    private IDbContextTransaction? _transaction;
    private readonly AdvisorDbContext _context;
    private readonly IRepository<Advisor> _advisorRepository;

    #endregion //Fields

    #region  Properties
    public IRepository<Advisor> Advisors => _advisorRepository;

    #endregion //Properties

    #region Constructors
    
    public UnitOfWork(AdvisorDbContext context, IRepository<Advisor> advisorRepository)
    {
        _context = context;
        _advisorRepository = advisorRepository;
    }
    #endregion //Constructors

    #region Methods

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> CommitAsync()
    {
        int result = -1;
        try
        {
            if (_transaction != null)
            {
                result = await _context.SaveChangesAsync();
                await _transaction!.CommitAsync();
            }

        }
        catch (Exception)
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        return result;
    }


    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    #endregion //Methods
}
