using FinanceTracker.DataAccess;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Services
{
    public class PaycheckService
    {
        private readonly FinanceTrackerContext _context;

        public PaycheckService(FinanceTrackerContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculateSalaryAfterTax(Job job)
        {
            decimal salaryBeforeTax = await CalculateSalaryBeforeTax(job);
            decimal taxDeduction = await CalculateTaxDeduction(job);
            decimal salaryAfterTax = salaryBeforeTax - taxDeduction;
            return salaryAfterTax;
        }

        public async Task<decimal> CalculateTaxDeduction(Job job)
        {
            decimal salaryBeforeTax = await CalculateSalaryBeforeTax(job);
            decimal taxRate = job.PaycheckInfo.Tax;
            decimal taxDeduction = salaryBeforeTax * taxRate;
            return taxDeduction;
        }

        public async Task<decimal> CalculateSalaryBeforeTax(Job job)
        {
            TimeSpan workedTime = await CalculateWorkedHours(job.PaycheckInfo);
            decimal totalHours = (decimal)workedTime.TotalHours;

            return totalHours * job.HourlyRate;
        }

        public async Task<TimeSpan> CalculateWorkedHours(PaycheckInfo paycheckInfo)
        {
            var workShifts = await GetWorkShiftsAsync(paycheckInfo.Id);

            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workShifts)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
            }

            return totalWorkedHours;
        }
        

        public async Task<List<WorkShift>> GetWorkShiftsAsync(int paycheckInfoId) // Tilføj validering af datoer
        {
            return await _context.PaycheckInfos
                .Where(p => p.Id == paycheckInfoId)
                .SelectMany(p => p.WorkShifts)
                .ToListAsync();
        }
    }
}
