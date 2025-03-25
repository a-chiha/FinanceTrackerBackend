using FinanceTracker.DataAccess.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.DataAccess.Services
{
    public class HolidayPayDataAccesService : IDataAccessService<HolidayPay>
    {
        public Task<IEnumerable<HolidayPay>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HolidayPay> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(HolidayPay entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(HolidayPay entity)
        {
            throw new NotImplementedException();
        }
    }

}
