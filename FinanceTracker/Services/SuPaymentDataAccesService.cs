using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public class SuPaymentDataAccesService : IDataAccessService<SuPayment>
    {
        public Task<IEnumerable<SuPayment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SuPayment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(SuPayment entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SuPayment entity)
        {
            throw new NotImplementedException();
        }
    }

}
