using System.Runtime.InteropServices.JavaScript;
using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Internal;


namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaychecksController : ControllerBase
    {

        private readonly IDataAccessService<WorkShift> _workShift;
        private readonly IDataAccessService<Job> _job;
        private readonly IDataAccessService<SupplementDetails> _supplementDetails;



        public PaychecksController(IDataAccessService<WorkShift> workshift, IDataAccessService<Job> job, IDataAccessService<SupplementDetails> supplementDetails)
        {
            _workShift = workshift;
            _job = job;
            _supplementDetails = supplementDetails;
        }
        [HttpGet]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GeneratePayCheckForMonth(string companyName, int month)
        {

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest("404 error");
            }

            var job = await _job.GetByIdAsync(companyName, UserId);
            var suppplementDetails = await _supplementDetails.GetFilteredAsync(x => x.Job == job);
            var workshiftsInMonth = await _workShift.GetFilteredAsync(w => w.StartTime.Month == month && w.UserId == UserId);

            if (workshiftsInMonth == null) return NotFound("could not find any workshifts for the specified month and companyname");



            TimeSpan totalWorkedHours = TimeSpan.Zero;
            decimal totalSupplementPay = 0;

            foreach (var workShift in workshiftsInMonth)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
                totalSupplementPay += CalculateSupplementPayForWorkshift(workShift, suppplementDetails);
            }

            decimal baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate + totalSupplementPay;

            decimal amcontribution = baseSalary * 0.08m;
            decimal salaryafterAM = baseSalary - amcontribution;
            decimal tax = 0.37m;
            decimal taxDeduction = salaryafterAM * tax;
            decimal salaryAfterTax = salaryafterAM - taxDeduction;



            var paycheck = new Paycheck()
            {
                SalaryBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours.TotalHours,
                AMContribution = amcontribution,
                Tax = tax,
                SalaryAfterTax = salaryAfterTax,

            };

            return Ok(paycheck);
        }

        private decimal CalculateSupplementPayForWorkshift(WorkShift workShift, IEnumerable<SupplementDetails> supplementDetails)
        {
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            int i = 0;
            var supplementDay = supplementDetails.FirstOrDefault(x => x.Weekday == workShift.StartTime.DayOfWeek);
            for (; i <= 24; i++)
            {
                if (i >= workShift.StartTime.Hour && i <= workShift.EndTime.Hour && i >= supplementDay.StartTime.Hour &&
                    i <= supplementDay.EndTime.Hour)
                {
                    startTime = workShift.StartTime.TimeOfDay > supplementDay.StartTime.TimeOfDay ? workShift.StartTime : supplementDay.StartTime;
                    break;
                }
            }
            if (startTime == DateTime.MinValue) return 0;

            endTime = workShift.EndTime.TimeOfDay < supplementDay.EndTime.TimeOfDay ? workShift.EndTime : supplementDay.EndTime;

            var timeSpace = endTime.TimeOfDay - startTime.TimeOfDay;
            decimal hoursWorked = (decimal)timeSpace.TotalHours;
            decimal hourlyRate = supplementDay.Amount; // e.g. 30
            decimal salary = hoursWorked * hourlyRate;
            return salary;
        }
        //                                                          ---------------------------------
        //                                     ----------------------------------------------
        // 10    11   12     13      14       15      16     17    18      19      20       21      22       23     
    }
}
