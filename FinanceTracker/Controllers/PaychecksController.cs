using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaychecksController : ControllerBase
    {

        private readonly IDataAccessService<WorkShift> _workShift;
        private readonly IDataAccessService<Job> _job;



        public PaychecksController(IDataAccessService<WorkShift> workshift, IDataAccessService<Job> job)
        {
            _workShift = workshift;
            _job = job;
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

            var workshiftsInMonth = await _workShift.GetFilteredAsync(w => w.StartTime.Month == month && w.UserId == UserId);
            if (workshiftsInMonth == null) return NotFound("could not find any workshifts for the specified month and companyname");

            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workshiftsInMonth)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
            }
            decimal baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate;
            decimal amcontribution = baseSalary * 0.08m;
            decimal salaryafterAM = baseSalary - amcontribution;
            decimal tax = 0.37m;
            decimal taxDeduction = salaryafterAM * tax;
            decimal salaryAfterTax = salaryafterAM - taxDeduction;



            var paycheck = new Paycheck()
            {
                SalaryBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours,
                AMContribution = amcontribution,
                Tax = tax,
                SalaryAfterTax = salaryAfterTax,

            };

            return Ok(paycheck);
        }



    }
}
