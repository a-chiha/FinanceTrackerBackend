using FinanceTracker.DTO;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mapster;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupplementDetailsController : ControllerBase
    {
        private readonly IDataAccessService<Job> _jobService;

        private readonly IDataAccessService<SupplementDetails> _supplementDetails;
        public SupplementDetailsController(IDataAccessService<Job> jobService, IDataAccessService<SupplementDetails> supplementDetails)
        {
            _jobService = jobService;
            _supplementDetails = supplementDetails;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterSupplementPay(List<SupplementDetailsDTO> supplementDetailsDTO,string companyName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            
            var job = await _jobService.GetByIdAsync(companyName,userId);

            for(int i =0;i<supplementDetailsDTO.Count;i++)
            {
                var sd = supplementDetailsDTO[i].Adapt<SupplementDetails>();
                sd.CompanyName = companyName;
                sd.Job = job;
               await _supplementDetails.AddAsync(sd);
            }
            return Ok(supplementDetailsDTO);
        }
    }
}
