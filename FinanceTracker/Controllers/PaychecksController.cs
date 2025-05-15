using System.Runtime.InteropServices.JavaScript;
using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;


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
                return BadRequest("Error");
            }

            var job = await _job.GetByIdAsync(companyName, UserId);
            if (job == null) return NotFound("could not find job");

            var (baseSalary, totalWorkedHours) = await CalculateSalaryBeforeTaxAndTotalHours((w => w.StartTime.Month == month && w.UserId == UserId), job);

            decimal amcontribution = baseSalary * 0.08m;
            decimal salaryafterAM = baseSalary - amcontribution;
            decimal taxDeduction = salaryafterAM * 0.37m;
            decimal salaryAfterTax = salaryafterAM - taxDeduction;
            decimal vacationPay = baseSalary * 0.125m;

            var paycheck = new Paycheck()
            {
                SalaryBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours.TotalHours,
                AMContribution = amcontribution,
                Tax = 0.37m,
                SalaryAfterTax = salaryAfterTax,
                VacationPay = vacationPay
            };

            return Ok(paycheck);
        }

        [HttpGet("VacationPay")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GetTotalVacationPay(string companyName, int year)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest("error");
            }

            var job = await _job.GetByIdAsync(companyName, UserId);
     

            var (salaryBeforeTax, WorkedHours) = await CalculateSalaryBeforeTaxAndTotalHours((w => w.StartTime.Year == year && w.UserId == UserId), job);


            var vacationPay = new VacationPayDTO()
            {
                VacationPay = salaryBeforeTax * 0.125m
            };


            return Ok(vacationPay);
        }

        private async Task<(decimal baseSalary, TimeSpan totalWorkedHours)> CalculateSalaryBeforeTaxAndTotalHours(Expression<Func<WorkShift, bool>> timeperiod, Job job)
        {
            var supplementDetails = await _supplementDetails.GetFilteredAsync(x => x.Job == job);

            var workshifts = await _workShift.GetFilteredAsync(timeperiod);
            if (workshifts == null) return (0, TimeSpan.Zero);
            TimeSpan totalWorkedHours = TimeSpan.Zero;
            decimal totalSupplementPay = 0;

            foreach (var workShift in workshifts)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
                totalSupplementPay += CalculateSupplementPayForWorkshift(workShift, supplementDetails);
            }
            var baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate + totalSupplementPay;
            return (baseSalary, totalWorkedHours);
        }



        private decimal CalculateSupplementPayForWorkshift(WorkShift workShift, IEnumerable<SupplementDetails> supplementDetails)
        {
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            int i = 0;
            var supplementDay = supplementDetails.FirstOrDefault(x => x.Weekday == workShift.StartTime.DayOfWeek);
            if (supplementDay == null) return 0;
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
     
    }
}
