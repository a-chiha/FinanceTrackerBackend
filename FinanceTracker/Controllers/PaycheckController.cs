using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Mapster;
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

        [HttpPost("workshift")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> RegisterWorkShift(WorkShiftDTO workshift)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _user.GetByIdAsync(UserId);

            var newWorkshift = workshift.Adapt<WorkShift>();
            newWorkshift.UserId = user.Id;
            newWorkshift.User = user;

            await _workShift.AddAsync(newWorkshift);

            return CreatedAtAction(nameof(RegisterWorkShift), newWorkshift);

        }

        [HttpGet("workshifts")]
        [Authorize]
        public async Task<IActionResult> GetAllWorkShiftsForUser()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var workshifts = await _workShift.GetFilteredAsync(x => x.UserId == UserId);

            return Ok(workshifts);
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

            var workshiftsInMonth = await _workShift.GetFilteredAsync(w => w.StartTime.Month == month && w.UserId == UserId);

            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workshiftsInMonth)
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



    }
}
