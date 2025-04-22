using FinanceTracker.DataAccess;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpPost("registerWorkshift")]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> RegisterWorkShift(WorkShift workShift, string userId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _user.GetByIdAsync(userId);
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
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GeneratePayCheckForMonth(int CVR, int month, string UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("404 error");
            }

            var job = await _job.GetByIdAsync(UserId, CVR);
            var workShifts = await _workShift.GetAllAsync();
            var filteredWorkshifts = workShifts.Where(w => w.StartTime.Month == month && w.UserId == UserId).ToList();
            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var workShift in workShifts)
            {
                totalWorkedHours += workShift.EndTime - workShift.StartTime;
            }
            decimal baseSalary = (decimal)totalWorkedHours.TotalHours * job.HourlyRate;
            decimal AMcontribution = baseSalary * 0.08m;
            decimal SalarayafterAM = baseSalary - AMcontribution;
            decimal tax = 0.37m;
            decimal taxDeduction = SalarayafterAM * tax;
            decimal salaryAfterTax = SalarayafterAM - taxDeduction;

            var paycheck = new Paycheck()
            {
                SalarayBeforeTax = baseSalary,
                WorkedHours = totalWorkedHours,
                AMContribution = AMcontribution,
                Tax = tax,
                SalarayAfterTax = salaryAfterTax

            };



            return Ok(paycheck);
        }



    }
}
