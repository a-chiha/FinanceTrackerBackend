using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.DataAccess.Interfaces;

namespace FinanceTracker.DataAccess.Repositories
{
    public class AccountDataAccesService : IDataAccessService<Account>
    {
        private readonly FinanceTrackerContext _context;

        public AccountDataAccesService(FinanceTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task AddAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
