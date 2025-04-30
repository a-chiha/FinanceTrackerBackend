using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaycheckController : ControllerBase
    {

        private readonly IDataAccessService<Paycheck> _paycheck;
        private readonly IDataAccessService<WorkShift> _workShift;
        private readonly IDataAccessService<FinanceUser> _user;
        private readonly IDataAccessService<Job> _job;



        public PaycheckController(IDataAccessService<Paycheck> paycheck, IDataAccessService<WorkShift> workshift, IDataAccessService<FinanceUser> user, IDataAccessService<Job> job)
        {
            _paycheck = paycheck;
            _workShift = workshift;
            _user = user;
            _job = job;
        }

        [HttpPost("RegisterWorkshift")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> RegisterWorkShift(WorkShift workShift)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _user.GetByIdAsync(UserId);
            var entity = new WorkShift
            {
                StartTime = workShift.StartTime,
                EndTime = workShift.EndTime,
                UserId = user.Id,
                User = user
            };

            await _workShift.AddAsync(entity);

            return CreatedAtAction(nameof(RegisterWorkShift), entity);

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

            var job = await _job.GetByIdAsync(UserId, companyName);
            var workShifts = await _workShift.GetAllAsync();
            var filteredWorkshifts = workShifts.Where(w => w.StartTime.Month == month && w.UserId == UserId).ToList();
            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workShifts)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
            }
            decimal baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate;
            decimal amcontribution = baseSalary * 0.08m;
            decimal salarayafterAM = baseSalary - amcontribution;
            decimal tax = 0.37m;
            decimal taxDeduction = salarayafterAM * tax;
            decimal salaryAfterTax = salarayafterAM - taxDeduction;

            var paycheck = new Paycheck()
            {
                SalarayBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours,
                AMContribution = amcontribution,
                Tax = tax,
                SalarayAfterTax = salaryAfterTax,

            };

            return Ok(paycheck);
        }

        [HttpGet("jobs")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GetAllUserJobs()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var allJobs = await _job.GetFilteredAsync(j => j.UserId == userId);
            
            return Ok(allJobs);
        }

        [HttpGet("job/{companyName}/month/{month}")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GeneratePaycheckForSpecificJob(string companyName, int month)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var job = await _job.GetByIdAsync(userId, companyName);
            
            if (job == null)
            {
                return NotFound($"Job at company '{companyName}' not found");
            }

            var workShifts = await _workShift.GetFilteredAsync(w => 
                w.UserId == userId && 
                w.StartTime.Month == month);
            
            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workShifts)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
            }
            
            decimal baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate;
            decimal amcontribution = baseSalary * 0.08m;
            decimal salaryAfterAM = baseSalary - amcontribution;
            decimal tax = 0.37m;
            decimal taxDeduction = salaryAfterAM * tax;
            decimal salaryAfterTax = salaryAfterAM - taxDeduction;

            var paycheck = new Paycheck()
            {
                SalarayBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours,
                AMContribution = amcontribution,
                Tax = tax,
                SalarayAfterTax = salaryAfterTax,
                taxDeduction = taxDeduction
            };

            return Ok(paycheck);
        }
    }
}
