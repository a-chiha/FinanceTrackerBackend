using FinanceTracker.Models;
using FinanceTracker.Interfaces;

namespace FinanceTracker.Services
{
    public class AccountDataAccesService : IDataAccessService<Account>
    {

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Account entity)
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
