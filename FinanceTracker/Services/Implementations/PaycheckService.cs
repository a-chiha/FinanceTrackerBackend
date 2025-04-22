//using FinanceTracker.DataAccess;
//using FinanceTracker.Models;
//using FinanceTracker.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FinanceTracker.Services.Implementations
//{
//    public class PaycheckService : IPaycheckService
//    {
//        private readonly FinanceTrackerContext _context;
//        private readonly IPaycheckValidation _validation;

//        public PaycheckService(FinanceTrackerContext context, IPaycheckValidation validation)
//        {
//            _context = context;
//            _validation = validation;
//        }

//        public async Task<decimal> CalculateSalaryAfterTax(Job job)
//        {
//            _validation.ValidateJob(job);

//            decimal salaryBeforeTax = await CalculateSalaryBeforeTax(job);
//            decimal taxDeduction = await CalculateTaxDeduction(job);
//            decimal salaryAfterTax = salaryBeforeTax - taxDeduction;
//            return salaryAfterTax;
//        }

//        public async Task<decimal> CalculateTaxDeduction(Job job)
//        {
//            _validation.ValidateJob(job);

//            decimal salaryBeforeTax = await CalculateSalaryBeforeTax(job);
//            decimal taxRate = job.PaycheckInfo.Tax;
//            decimal taxDeduction = salaryBeforeTax * taxRate;

//            return taxDeduction;
//        }

//        public async Task<decimal> CalculateSalaryBeforeTax(Job job)
//        {
//            _validation.ValidateJob(job);

//            TimeSpan workedTime = await CalculateWorkedHours(job.PaycheckInfo);
//            decimal totalHours = (decimal)workedTime.TotalHours;

//            return totalHours * job.HourlyRate;
//        }

//        public async Task<TimeSpan> CalculateWorkedHours(PaycheckInfo paycheckInfo)
//        {
//            _validation.ValidatePaycheckInfo(paycheckInfo);

//            var workShifts = await GetWorkShiftsAsync(paycheckInfo.Id);

//            TimeSpan totalWorkedHours = TimeSpan.Zero;
//            foreach (var workShift in workShifts)
//            {
//                try
//                {
//                    _validation.ValidateWorkShift(workShift);
//                    totalWorkedHours += workShift.EndTime - workShift.StartTime;
//                }
//                catch (ArgumentException ex)
//                {
//                    Console.WriteLine($"Habibi der noget galt med dine workshifts");
//                }
//            }

//            return totalWorkedHours;
//        }

//        //public async Task<List<WorkShift>> GetWorkShiftsAsync(int paycheckId) // Tilføj validering af datoer
//        //{
//        //    return await _context.Paychecks
//        //        .Where(p => p. == paycheckId)
//        //        .SelectMany(p => p.WorkShifts)
//        //        .ToListAsync();
//        //}
//    }
//}
