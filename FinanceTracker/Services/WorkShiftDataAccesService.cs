using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public class WorkShiftDataAccesService : IDataAccessService<WorkShift>
    {
        public Task<IEnumerable<WorkShift>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkShift> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(WorkShift entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkShift entity)
        {
            throw new NotImplementedException();
        }
    }

}
