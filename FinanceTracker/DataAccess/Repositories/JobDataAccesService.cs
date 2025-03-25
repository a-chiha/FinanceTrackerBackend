using FinanceTracker.DataAccess.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.DataAccess.Services
{
    public class JobDataAccesService : IDataAccessService<Job>
    {
        public Task<IEnumerable<Job>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Job entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Job entity)
        {
            throw new NotImplementedException();
        }
    }

}
