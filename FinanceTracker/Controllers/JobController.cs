using System.Security.Claims;
using FinanceTracker.DTO;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class JobController : ControllerBase
    {
        private readonly IDataAccessService<Job> _job;
        private readonly IDataAccessService<FinanceUser> _user;

        public JobController(IDataAccessService<Job> job, IDataAccessService<FinanceUser> financeUser)
        {
            _job = job;
            _user = financeUser;
        }

        [HttpPost("RegisterJob")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> RegisterJob(JobDTO job)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _user.GetByIdAsync(userId);
            var entity = new Job()
            {
                CompanyName = job.CompanyName,
                HourlyRate = job.HourlyRate,
                EmploymentType = job.EmploymentType,
                TaxCard = job.TaxCard,
                UserId = userId,
                User = user
            };
            await _job.AddAsync(entity);
            return Ok(entity);
        }

        [HttpPost("UpdateJob")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> UpdateJob(JobDTO job)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var jobToUpdate = await _job.GetFirstOrDefaultAsync(x => x.UserId == userId && x.CompanyName == job.CompanyName);
            if (jobToUpdate == null)
            {
                return NotFound("Job not found");
            }
            jobToUpdate.CompanyName = job.CompanyName;
            jobToUpdate.HourlyRate = job.HourlyRate;
            jobToUpdate.EmploymentType = job.EmploymentType;
            jobToUpdate.TaxCard = job.TaxCard;
            await _job.UpdateAsync(jobToUpdate);
            return Ok(jobToUpdate);
        }

        [HttpGet("GetAllJobsForUser")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GetAllJobsForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest("error bad model");
            }
            var userJobs = await _job.GetFilteredAsync(x => x.UserId == userId);
            if (userJobs == null)
            {
                return NotFound("Job not found for ");
            }
            
            return  Ok(userJobs);

        }

        [HttpDelete("DeleteJob")]
        [Authorize]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> DeleteJob(string companyName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (companyName.IsNullOrEmpty()) return BadRequest("error please provide a company name");
            var jobToDelete = await _job.GetByIdAsync(companyName, userId);
            if (jobToDelete == null) return NotFound("Job not found");
           await _job.DeleteAsync(jobToDelete);
            return Ok();
        }

    }
}
