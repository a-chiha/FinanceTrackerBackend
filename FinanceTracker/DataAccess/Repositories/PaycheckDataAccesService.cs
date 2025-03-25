using FinanceTracker.DataAccess.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.DataAccess.Services
{
    public class PaycheckDataAccesService : IDataAccessService<Paycheck>
    {
        public Task<IEnumerable<Paycheck>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Paycheck> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Paycheck entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Paycheck entity)
        {
            throw new NotImplementedException();
        }
    }
}
