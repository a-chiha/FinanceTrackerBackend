using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public class SupplementPayDataAccesService : IDataAccessService<SupplementPay>
    {
        public Task<IEnumerable<SupplementPay>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplementPay> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(SupplementPay entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SupplementPay entity)
        {
            throw new NotImplementedException();
        }
    }

}
