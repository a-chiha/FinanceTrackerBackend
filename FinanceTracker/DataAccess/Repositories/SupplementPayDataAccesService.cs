using FinanceTracker.DataAccess.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.DataAccess.Services
{
    public class SupplementPayDataAccesService : IDataAccessService<SupplementDetails>
    {
        public Task<IEnumerable<SupplementDetails>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplementDetails> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(SupplementDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SupplementDetails entity)
        {
            throw new NotImplementedException();
        }
    }

}
